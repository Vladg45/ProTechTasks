using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class BruteForceNearest : IDriverFinderAlg
    {
        public List<Driver> FindDrivers(IEnumerable<Driver> drivers, Order order, int count)
        {
            if (drivers == null || !drivers.Any() || order == null || count <= 0)
                return new List<Driver>();

            return drivers.OrderBy(driver => DistanceHelper.CalculateSquaredDistance(driver.X, driver.Y, order.X, order.Y))
                           .Take(count)
                           .ToList();
        }
    }
}