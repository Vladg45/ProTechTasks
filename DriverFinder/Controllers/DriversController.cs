using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DriverFinder.Models;
using DriverFinder.Algorithms;

namespace DriverFinder.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : Controller
    {
        private readonly IDriverFinderAlg _bruteForceNearest;
        private readonly IDriverFinderAlg _priorityQueueNearest;
        private readonly IDriverFinderAlg _quickSelectNearest;
        private readonly IDriverFinderAlg _radiusExpansionNearest;

        public DriversController()
        {
            _bruteForceNearest = new BruteForceNearest();
            _priorityQueueNearest = new PriorityQueueNearest();
            _quickSelectNearest = new QuickSelectNearest();
            _radiusExpansionNearest = new RadiusExpansionNearest();
        }
        
        [HttpPost("nearest/bruteforce")]
        public ActionResult<List<Driver>> FindNearestBruteFouce([FromBody] Order order)
        {
            var drivers = GetTestDrivers();
            var result = _bruteForceNearest.FindDrivers(drivers, order, 5);
            return Ok(result);
        }

        [HttpPost("nearest/priorityqueue")]
        public ActionResult<List<Driver>> FindNearestPriorityQueue([FromBody] Order order)
        {
            var drivers = GetTestDrivers();
            var result = _priorityQueueNearest.FindDrivers(drivers, order, 5);
            return Ok(result);
        }

        [HttpPost("nearest/quickselect")]
        public ActionResult<List<Driver>> FindNearestQuickSelect([FromBody] Order order)
        {
            var drivers = GetTestDrivers();
            var result = _quickSelectNearest.FindDrivers(drivers, order, 5);
            return Ok(result);
        }

        [HttpPost("nearest/radiusexpansion")]
        public ActionResult<List<Driver>> FindNearestRadiusExpansion([FromBody] Order order)
        {
            var drivers = GetTestDrivers();
            var result = _radiusExpansionNearest.FindDrivers(drivers, order, 5);
            return Ok(result);
        }

        // Тестовый набор водителей
        private List<Driver> GetTestDrivers()
        {
            return new List<Driver>
            {
            new Driver { Id = 1, X = 1, Y = 1 },
            new Driver { Id = 2, X = 5, Y = 5 },
            new Driver { Id = 3, X = 10, Y = 10 },
            new Driver { Id = 4, X = 2, Y = 2 },
            new Driver { Id = 5, X = 8, Y = 8 },
            new Driver { Id = 6, X = 15, Y = 15 },
            new Driver { Id = 7, X = 3, Y = 3 },
            new Driver { Id = 8, X = 12, Y = 12 },
            new Driver { Id = 9, X = 7, Y = 7 },
            new Driver { Id = 10, X = 4, Y = 4 }
            };
        }
    }
}
