using DriverFinder.Models;

namespace DriverFinder.Algorithms
{
    public class PriorityQueueNearest : IDriverFinderAlg
    {
        public List<Driver> FindDrivers(IEnumerable<Driver> drivers, Order order, int count)
        {
            if (drivers == null || !drivers.Any() || order == null || count <= 0)
                return new List<Driver>();

            // Очередь с высшим приоритетом для минимального числа
            var heap = new PriorityQueue<Driver, int>(
                Comparer<int>.Create((a, b) => b.CompareTo(a)));

            foreach (var driver in drivers)
            {
                int distance = DistanceHelper.CalculateSquaredDistance(driver.X, driver.Y, order.X, order.Y);

                if (heap.Count < count)
                {
                    heap.Enqueue(driver, distance);
                }
                else if (heap.TryPeek(out _, out int currentPriority))
                {
                    if (distance < currentPriority)
                    {
                        heap.Dequeue();
                        heap.Enqueue(driver, distance);
                    }
                }
            }

            var result = new List<Driver>();
            while (heap.Count > 0)
            {
                result.Add(heap.Dequeue());
            }

            result.Reverse();
            return result;
        }
    }
}