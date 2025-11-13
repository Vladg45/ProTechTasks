using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public interface IDriverFinderAlg
    {
        List<Driver> FindDrivers(IEnumerable<Driver> drivers, Order order, int count);
    }
}
