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
    public class InventoryServiceTests
    {
        private Mock<IInventoryRepository> _mockRepo;
        private InventoryService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IInventoryRepository>();
            _service = new InventoryService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task AddNewProductTransactionAsync_CallsRepoOnce()
        {
            var product = new Product("Test", 10m, 1);
            var inventory = new Inventory(1);

            await _service.AddNewProductTransactionAsync(product, inventory);

            _mockRepo.Verify(r => r.AddNewProductTransactionAsync(product, inventory), Times.Once);
        }

        [TestMethod]
        public async Task AddNewProductTransactionAsync_ThrowsQueryFailedException()
        {
            var product = new Product("Test", 10m, 1);
            var inventory = new Inventory(1);

            _mockRepo.Setup(r => r.AddNewProductTransactionAsync(product, inventory))
                     .ThrowsAsync(new QueryFailedException("fail"));

            await Assert.ThrowsExceptionAsync<QueryFailedException>(() =>
                _service.AddNewProductTransactionAsync(product, inventory));
        }

        [TestMethod]
        public async Task AddStockAsync_CallsRepoOnce()
        {
            var inventory = new Inventory(1, 10, DateTime.Now);

            await _service.AddStockAsync(inventory);

            _mockRepo.Verify(r => r.AddStockAsync(inventory), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_ByWarehouseId_ReturnsExpected()
        {
            var expected = new List<Inventory> { new Inventory(1), new Inventory(2) };
            _mockRepo.Setup(r => r.GetAllAsync(1)).ReturnsAsync(expected);

            var result = await _service.GetAllAsync(1);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectInventory()
        {
            var inventory = new Inventory(1, 2, 3, 10, DateTime.Today);
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(inventory);

            var result = await _service.GetByIdAsync(1);

            Assert.AreEqual(1, result.InventoryId);
        }
    }
}
