using DAL.Interfaces;
using Domain;
using Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using Service.Interfaces;
using Service.Utility;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class ZoneServiceTests
    {
        private Mock<IZoneRepository> _mockRepo;
        private ZoneService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IZoneRepository>();
            _service = new ZoneService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCallRepositoryAdd()
        {
            var zone = new Zone("Zone A", 100, warehouseId: 1);

            await _service.CreateAsync(zone);

            _mockRepo.Verify(r => r.AddAsync(zone), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldThrow_WhenQueryFails()
        {
            // Arrange
            var zone = new Zone("Zone A", 100, warehouseId: 1);
            _mockRepo.Setup(r => r.AddAsync(zone)).ThrowsAsync(new QueryFailedException("Query failed"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<QueryFailedException>(() =>
                _service.CreateAsync(zone));
        }


        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllZones()
        {
            var zones = new List<Zone>
        {
            new Zone(1, "Zone A", 50, 1),
            new Zone(2, "Zone B", 70, 1)
        };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(zones);

            var result = await _service.GetAllAsync();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetAllAsync_ByWarehouse_ShouldReturnFilteredZones()
        {
            int warehouseId = 5;
            var zones = new List<Zone>
        {
            new Zone(3, "Zone W", 150, warehouseId)
        };
            _mockRepo.Setup(r => r.GetAllAsync(warehouseId)).ReturnsAsync(zones);

            var result = await _service.GetAllAsync(warehouseId);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Zone W", result.First().Name);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectZone()
        {
            int zoneId = 10;
            var zone = new Zone(zoneId, "Zone Z", 300, 2);
            _mockRepo.Setup(r => r.GetByIdAsync(zoneId)).ReturnsAsync(zone);

            var result = await _service.GetByIdAsync(zoneId);

            Assert.IsNotNull(result);
            Assert.AreEqual(zoneId, result.ZoneId);
        }
    }
}
