using DriverFinder.Tests.Algorithms;

namespace DriverFinder.Tests.Tests
{
    [TestFixture]
    public class BruteForceNearestTests : BaseDriverFinderTests<BruteForceNearest> { }

    [TestFixture]
    public class PriorityQueueNearestTests : BaseDriverFinderTests<PriorityQueueNearest> { }

    [TestFixture]
    public class QuickSelectNearestTests : BaseDriverFinderTests<QuickSelectNearest> { }

    [TestFixture]
    public class RadiusExpansionNearestTests : BaseDriverFinderTests<RadiusExpansionNearest> { }
}
