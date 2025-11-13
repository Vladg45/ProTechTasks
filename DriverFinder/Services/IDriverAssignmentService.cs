using DriverFinder.Models;
using DriverFinder.Algorithms;

namespace DriverFinder.Services
{
    public interface IDriverAssignmentService
    {
        Task<DriverAssignmentResponse?> AssignDriverToOrderAsync(OrderRequest order, int mapWidth, int mapHeight);
    }

    public class DriverAssignmentService : IDriverAssignmentService
    {
        private readonly IDriverService _driverService;
        private readonly IRandomNumberService _randomNumberService;
        private readonly IRouteCalculatorService _routeCalculator;
        private readonly IDriverFinderAlg _driverFinder;
        private readonly ILogger<DriverAssignmentService> _logger;

        public DriverAssignmentService(
            IDriverService driverService,
            IRandomNumberService randomNumberService,
            IRouteCalculatorService routeCalculator,
            ILogger<DriverAssignmentService> logger)
        {
            _driverService = driverService;
            _randomNumberService = randomNumberService;
            _routeCalculator = routeCalculator;
            _driverFinder = new QuickSelectNearest();
            _logger = logger;
        }

        public async Task<DriverAssignmentResponse?> AssignDriverToOrderAsync(OrderRequest order, int mapWidth, int mapHeight)
        {
            _logger.LogInformation($"Поиск водителя для заказа {order.Id} на координатах ({order.X}, {order.Y})");

            // Получение всех доступных водителей
            var allDrivers = _driverService.GetAllDrivers();

            _logger.LogInformation($"Найдено всего водителей: {allDrivers.Count}");

            if (!allDrivers.Any())
            {
                _logger.LogWarning("Свободных водителей нет");
                return null;
            }

            // Поиск ближайших водителей
            var orderForSearch = new Order { X = order.X, Y = order.Y };
            var nearestDrivers = _driverFinder.FindDrivers(allDrivers, orderForSearch, Math.Min(5, allDrivers.Count));

            _logger.LogInformation($"Найдено ближайших водителей: {nearestDrivers.Count}");

            if (!nearestDrivers.Any())
            {
                _logger.LogWarning("Не найдено подходящих водителей");
                return null;
            }

            // Получение случайного числа для выбора водителя
            try
            {   
                int randomIndex = await _randomNumberService.GetRandomNumberAsync(0, nearestDrivers.Count);
                _logger.LogInformation($"Выбран случайный индекс: {randomIndex} из {nearestDrivers.Count} водителей");

                // Проверка корректности индекса
                if (randomIndex < 0 || randomIndex >= nearestDrivers.Count)
                {
                    _logger.LogWarning($"Некорректный случайный индекс: {randomIndex}, используем 0");
                    randomIndex = 0;
                }

                var selectedDriver = nearestDrivers[randomIndex];

                // Расчет маршрута
                var route = _routeCalculator.CalculateRoute(
                    selectedDriver.X, selectedDriver.Y, order.X, order.Y);

                var routeLength = _routeCalculator.CalculateRouteLength(route);

                _logger.LogInformation($"Рассчитан маршрут длиной {routeLength} шагов");

                return new DriverAssignmentResponse
                {
                    DriverId = selectedDriver.Id,
                    DriverX = selectedDriver.X,
                    DriverY = selectedDriver.Y,
                    RouteLength = routeLength,
                    Route = route
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выборе водителя");
                return null;
            }
        }
    }
}
