using BenchmarkDotNet.Attributes;
using DriverFinder.Models;
using DriverFinder.Algorithms;

namespace DriverFinder.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 2, iterationCount: 5)]
    public class AlgorithmBenchmarks
    {
        private List<Driver> _drivers = null!;
        private Order _testOrder = null!;

        private readonly IDriverFinderAlg _bruteForceNearest;
        private readonly IDriverFinderAlg _priorityQueueNearest;
        private readonly IDriverFinderAlg _quickSelectNearest;
        private readonly IDriverFinderAlg _radiusExpansionNearest;

        public AlgorithmBenchmarks()
        {
            _bruteForceNearest = new BruteForceNearest();
            _priorityQueueNearest = new PriorityQueueNearest();
            _quickSelectNearest = new QuickSelectNearest();
            _radiusExpansionNearest = new RadiusExpansionNearest();
        }

        [GlobalSetup]
        public void Setup()
        {
            _drivers = Generate.TestDrivers(1000); // Тестируем на 1000 водителей
            _testOrder = new Order { X = 50, Y = 50 }; // Заказ
        }

        [Params(5, 10, 20)]
        public int Count { get; set; }

        [Benchmark]
        public List<Driver> BruteForceNearest()
        {
            return _bruteForceNearest.FindDrivers(_drivers, _testOrder, Count);
        }

        [Benchmark]
        public List<Driver> PriorityQueueNearest()
        {
            return _priorityQueueNearest.FindDrivers(_drivers, _testOrder, Count);
        }

        [Benchmark]
        public List<Driver> QuickSelectNearest()
        {
            return _quickSelectNearest.FindDrivers(_drivers, _testOrder, Count);
        }

        [Benchmark]
        public List<Driver> RadiusExpansionNearest()
        {
            return _radiusExpansionNearest.FindDrivers(_drivers, _testOrder, Count);
        }
    }
}