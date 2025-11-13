using BenchmarkDotNet.Attributes;
using DriverFinder.Algorithms;
using DriverFinder.Models;

namespace DriverFinder.Benchmarks
{
    [MemoryDiagnoser]
    public class ScalabilityBenchmarks
    {
        private readonly IDriverFinderAlg _bruteForce = new BruteForceNearest();
        private readonly IDriverFinderAlg _priorityQueue = new PriorityQueueNearest();
        private readonly IDriverFinderAlg _quickSelect = new QuickSelectNearest();
        private readonly IDriverFinderAlg _radiusExpansion = new RadiusExpansionNearest();

        [Params(100, 1000, 10000)]
        public int DriverCount { get; set; }

        private List<Driver> _drivers = null!;
        private Order _testOrder = null!;

        [GlobalSetup]
        public void Setup()
        {
            _drivers = Generate.TestDrivers(DriverCount);
            _testOrder = new Order { X = 50, Y = 50 };
        }

        [Benchmark]
        public List<Driver> BruteForce_Scalability() => _bruteForce.FindDrivers(_drivers, _testOrder, 10);

        [Benchmark]
        public List<Driver> PriorityQueue_Scalability() => _priorityQueue.FindDrivers(_drivers, _testOrder, 10);

        [Benchmark]
        public List<Driver> QuickSelect_Scalability() => _quickSelect.FindDrivers(_drivers, _testOrder, 10);

        [Benchmark]
        public List<Driver> RadiusExpansion_Scalability() => _radiusExpansion.FindDrivers(_drivers, _testOrder, 10);
    }
}