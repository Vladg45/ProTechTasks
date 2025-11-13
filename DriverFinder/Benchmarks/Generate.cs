using DriverFinder.Models;

namespace DriverFinder.Benchmarks
{
    public static class Generate
    {
        // Генерация новых водителей с разными координатами
        public static List<Driver> TestDrivers(int count)
        {
            var random = new Random(42);
            var drivers = new List<Driver>();
            var usedCoordinates = new HashSet<(int, int)>();

            for (int i = 0; i < count; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(0, 100);
                    y = random.Next(0, 100);
                } while (usedCoordinates.Contains((x, y)));

                usedCoordinates.Add((x, y));

                drivers.Add(new Driver
                {
                    Id = i,
                    X = x,
                    Y = y
                });
            }

            return drivers;
        }
    }
}
