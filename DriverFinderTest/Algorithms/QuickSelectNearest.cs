using DriverFinder.Tests.Models;

namespace DriverFinder.Tests.Algorithms
{
    public class QuickSelectNearest : IDriverFinderAlg
    {
        public List<Driver> FindDrivers(IEnumerable<Driver> drivers, Order order, int count)
        {
            if (drivers == null || !drivers.Any() || order == null || count <= 0)
                return new List<Driver>();

            var driversList = drivers.ToList();
            if (driversList.Count <= count)
                return driversList.Take(count).ToList();

            QuickSelect(driversList, order, 0, driversList.Count - 1, count);

            return driversList.Take(count)
                .OrderBy(driver => DistanceHelper.CalculateSquaredDistance(
                    driver.X, driver.Y, order.X, order.Y))
                .ToList();
        }

        private void QuickSelect(List<Driver> drivers, Order order, int left, int right, int k)
        {
            if (left >= right) return;

            int pivotIndex = Partition(drivers, order, left, right);

            if (pivotIndex == k) return;
            else if (pivotIndex < k)
                QuickSelect(drivers, order, pivotIndex + 1, right, k);
            else
                QuickSelect(drivers, order, left, pivotIndex - 1, k);
        }
        
        private int Partition(List<Driver> drivers, Order order, int left, int right)
        {
            var pivot = drivers[right];
            int pivotDistance = DistanceHelper.CalculateSquaredDistance(
                pivot.X, pivot.Y, order.X, order.Y);

            int i = left;
            for (int j = left; j < right; j++)
            {
                int currentDistance = DistanceHelper.CalculateSquaredDistance(
                    drivers[j].X, drivers[j].Y, order.X, order.Y);

                if (currentDistance < pivotDistance)
                {
                    (drivers[i], drivers[j]) = (drivers[j], drivers[i]);
                    i++;
                }
            }

            (drivers[i], drivers[right]) = (drivers[right], drivers[i]);
            return i;
        }
    }
}
