using DriverFinder.Tests.Models;
using DriverFinder.Tests.Algorithms;

namespace DriverFinder.Tests
{
    [TestFixture]
    public abstract class BaseDriverFinderTests<T> where T : IDriverFinderAlg, new()
    {
        protected IDriverFinderAlg _algorithm;
        protected List<Driver> _drivers;
        protected Order _order;

        [SetUp]
        public void Setup()
        {
            _algorithm = new T();
            _drivers = new List<Driver>
            {
                new Driver { Id = 1, X = 1, Y = 1 },
                new Driver { Id = 2, X = 5, Y = 5 },
                new Driver { Id = 3, X = 10, Y = 10 },
                new Driver { Id = 4, X = 2, Y = 2 },
                new Driver { Id = 5, X = 8, Y = 8 },
                new Driver { Id = 6, X = 15, Y = 15 },
                new Driver { Id = 7, X = 3, Y = 3 },
                new Driver { Id = 8, X = 12, Y = 12 },
                new Driver { Id = 9, X = 7, Y = 7 },
                new Driver { Id = 10, X = 4, Y = 4 },
            };
            _order = new Order { X = 0, Y = 0 };
        }

        [Test]
        public void FindDrivers_ShouldReturnNearestDrivers()
        {
            // Act
            var result = _algorithm.FindDrivers(_drivers, _order, 3);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));

            // Проверяем, что возвращены ближайшие по расстоянию водители
            var expectedIds = new[] { 1, 4, 7 }; // (1,1), (2,2), (3,3)
            CollectionAssert.AreEqual(expectedIds, result.Select(d => d.Id).ToArray());
        }

        [Test]
        public void FindDrivers_WithEmptyList_ReturnsEmpty()
        {
            // Act
            var result = _algorithm.FindDrivers(new List<Driver>(), _order, 5);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindDrivers_WithNullDrivers_ReturnsEmpty()
        {
            // Act
            var result = _algorithm.FindDrivers(null, _order, 5);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindDrivers_WithNullOrder_ReturnsEmpty()
        {
            // Act
            var result = _algorithm.FindDrivers(_drivers, null, 5);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindDrivers_WithZeroOrNegativeCount_ReturnsEmpty()
        {
            // Act
            var resultZero = _algorithm.FindDrivers(_drivers, _order, 0);
            var resultNegative = _algorithm.FindDrivers(_drivers, _order, -1);

            // Assert
            Assert.That(resultZero, Is.Empty);
            Assert.That(resultNegative, Is.Empty);
        }

        [Test]
        public void FindDrivers_WithCountGreaterThanDrivers_ReturnsAll()
        {
            // Act
            var result = _algorithm.FindDrivers(_drivers, _order, 50);

            // Assert
            Assert.That(result.Count, Is.EqualTo(_drivers.Count));
        }
    }
}