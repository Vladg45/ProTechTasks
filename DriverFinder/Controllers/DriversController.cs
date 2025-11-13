using Microsoft.AspNetCore.Mvc;
using DriverFinder.Models;
using DriverFinder.Algorithms;
using DriverFinder.Services;

namespace DriverFinder.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : Controller
    {
        private readonly IDriverFinderAlg _quickSelectNearest;
        private readonly IDriverService _driverService;
        private readonly IDriverAssignmentService _driverAssignmentService;
        private readonly MapSettings _mapSettings;

        public DriversController(IDriverService driverService, IDriverAssignmentService driverAssignmentService, IConfiguration configuration)
        {
            _quickSelectNearest = new QuickSelectNearest();
            _driverService = driverService;
            _driverAssignmentService = driverAssignmentService;

            // Загружаем настройки карты
            _mapSettings = configuration.GetSection("MapSettings").Get<MapSettings>()
                ?? new MapSettings { N = 100, M = 100 }; // значения по умолчанию
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignDriverToOrder([FromBody] OrderRequest orderRequest)
        {
            if (orderRequest.X < 0 || orderRequest.X >= _mapSettings.N ||
                orderRequest.Y < 0 || orderRequest.Y >= _mapSettings.M)
            {
                return BadRequest(new { error = "Координаты некорректны" });
            }

            // Поиск водителя для заказа
            var assignment = await _driverAssignmentService.AssignDriverToOrderAsync(
                orderRequest, _mapSettings.N, _mapSettings.M);

            if (assignment == null)
            {
                return BadRequest(new { error = "Свободных водителей нет" });
            }

            return Ok(assignment);
        }

        [HttpPost("coordinates")]
        public IActionResult UpdateDriverCoordinates([FromBody] UpdateDriverCoordinatesRequest request)
        {
            // Проверяем корректность координат
            if (request.X < 0 || request.X >= _mapSettings.N || request.Y < 0 || request.Y >= _mapSettings.M)
            {
                return BadRequest(new { error = "Координаты некорректны" });
            }

            // Проверяем, заняты ли координаты другим водителем
            if (_driverService.IsCoordinateOccupied(request.X, request.Y, request.Id))
            {
                return BadRequest(new { error = "Здесь уже находится другой водитель" });
            }

            var existingDriver = _driverService.GetDriverById(request.Id);
            bool isUpdate = existingDriver != null;

            // Обновляем координаты
            bool success = _driverService.UpdateDriverCoordinates(
                request.Id, request.X, request.Y, _mapSettings.N, _mapSettings.M);

            if (!success)
            {
                return BadRequest(new { error = "Не удалось обновить координаты" });
            }

            string message = isUpdate ? "Координаты успешно изменены" : "Координаты успешно добавлены";
            return Ok(new { message = message });
        }

        [HttpPost("findnearests")]
        public ActionResult<List<Driver>> FindNearestQuickSelect([FromBody] Order order)
        {
            var drivers = _driverService.GetAllDrivers();
            var result = _quickSelectNearest.FindDrivers(drivers, order, 5);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<Driver>> GetAllDrivers()
        {
            var drivers = _driverService.GetAllDrivers();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public ActionResult<Driver> GetDriverById(int id)
        {
            var driver = _driverService.GetDriverById(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }
    }
}
