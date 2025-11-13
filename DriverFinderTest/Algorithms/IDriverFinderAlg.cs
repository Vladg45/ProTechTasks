using DriverFinder.Tests.Models;

namespace DriverFinder.Tests.Algorithms
{
    public interface IDriverFinderAlg
    {
        List<Driver> FindDrivers(IEnumerable<Driver> drivers, Order order, int count);
    }
}
