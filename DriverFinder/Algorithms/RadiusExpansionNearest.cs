using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class RadiusExpansionNearest : IDriverFinderAlg
    {
        public List<Driver> FindDrivers(IEnumerable<Driver> drivers, Order order, int count)
        {
            if (drivers == null || !drivers.Any() || order == null || count <= 0)
                return new List<Driver>();

            var driverGrid = drivers.ToDictionary(d => (d.X, d.Y));
            var result = new List<Driver>();
            int radius = 0;

            while (result.Count < count && radius < 1000)
            {
                // Ищем водителей на расстоянии radius
                for (int dx = -radius; dx <= radius; dx++)
                {
                    int x = order.X + dx;
                    int y1 = order.Y + (radius - Math.Abs(dx));
                    int y2 = order.Y - (radius - Math.Abs(dx));

                    CheckAndAddDriver(driverGrid, x, y1, result);
                    if (y1 != y2) CheckAndAddDriver(driverGrid, x, y2, result);
                }

                radius++;
            }

            return result.Take(count).ToList();
        }

        private void CheckAndAddDriver(Dictionary<(int, int), Driver> grid, int x, int y, List<Driver> result)
        {
            if (grid.TryGetValue((x, y), out var driver))
                result.Add(driver);
        }
    }
}
