using DriverFinder.Models;

namespace DriverFinder.Services
{
    public interface IDriverService
    {
        Driver? GetDriverById(int id);
        List<Driver> GetAllDrivers();
        bool UpdateDriverCoordinates(int driverId, int x, int y, int mapWidth, int mapHeight);
        bool IsCoordinateOccupied(int x, int y, int? excludeDriverId = null);
    }

    public class DriverService : IDriverService
    {
        private readonly List<Driver> _drivers = new();
        private readonly object _lock = new();

        public Driver? GetDriverById(int id)
        {
            lock (_lock)
            {
                return _drivers.FirstOrDefault(d => d.Id == id);
            }
        }

        public List<Driver> GetAllDrivers()
        {
            lock (_lock)
            {
                return new List<Driver>(_drivers);
            }
        }

        public bool UpdateDriverCoordinates(int driverId, int x, int y, int mapWidth, int mapHeight)
        {
            lock (_lock)
            {
                var existingDriver = _drivers.FirstOrDefault(d => d.Id == driverId);

                if (existingDriver != null)
                {
                    // Обновление существующего водителя
                    if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                    {
                        // Координаты за пределами карты
                        _drivers.Remove(existingDriver);
                        return true;
                    }

                    if (IsCoordinateOccupied(x, y, driverId))
                    {
                        // Координаты заняты
                        return false;
                    }

                    // Обновляем координаты
                    existingDriver.X = x;
                    existingDriver.Y = y;
                }
                else
                {
                    // Добавление нового водителя
                    if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                    {
                        // Координаты за пределами карты
                        return false;
                    }

                    if (IsCoordinateOccupied(x, y))
                    {
                        // Координаты заняты
                        return false;
                    }

                    _drivers.Add(new Driver { Id = driverId, X = x, Y = y });
                }

                return true;
            }
        }

        public bool IsCoordinateOccupied(int x, int y, int? excludeDriverId = null)
        {
            return _drivers.Any(d =>
                d.X == x && d.Y == y &&
                (excludeDriverId == null || d.Id != excludeDriverId));
        }
    }
}
