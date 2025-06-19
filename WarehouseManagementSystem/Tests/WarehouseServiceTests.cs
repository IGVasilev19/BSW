using DAL.Interfaces;
using Domain;
using Exceptions;
using Microsoft.Data.SqlClient;
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
    public class WarehouseServiceTests
    {
        private Mock<IWarehouseRepository> _mockRepo;
        private WarehouseService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IWarehouseRepository>();
            _service = new WarehouseService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllWarehouses()
        {
            var warehouses = new List<Warehouse>
        {
            new Warehouse(1, "A", 100),
            new Warehouse(2, "B", 101)
        };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(warehouses);

            var result = await _service.GetAllAsync();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("A", result.First().Name);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnWarehouse_WhenFound()
        {
            var warehouse = new Warehouse(1, "Alpha", 555);
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouse);

            var result = await _service.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Alpha", result.Name);
            Assert.AreEqual(555, result.AddressId);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCallAdd()
        {
            var warehouse = new Warehouse("NewWarehouse", 888);
            await _service.CreateAsync(warehouse);

            _mockRepo.Verify(r => r.AddAsync(warehouse), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsync_WithTransaction_ShouldReturnGeneratedId()
        {
            var warehouse = new Warehouse("Test Warehouse", 123);
            var connection = new SqlConnection("Server=localhost;Database=TestDb;Trusted_Connection=True;");
            await connection.OpenAsync(); 

            var transaction = connection.BeginTransaction();

            _mockRepo
                .Setup(r => r.AddAsync(warehouse, connection, transaction))
                .ReturnsAsync(42); 

            var result = await _service.CreateAsync(warehouse, connection, transaction);

            Assert.AreEqual(42, result);

            transaction.Dispose();
            connection.Dispose();
        }


        [TestMethod]
        public async Task UpdateAsync_ShouldCallUpdate()
        {
            var warehouse = new Warehouse(5, "Updated", 202);
            await _service.UpdateAsync(warehouse);

            _mockRepo.Verify(r => r.UpdateAsync(warehouse), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldCallDelete()
        {
            await _service.DeleteByIdAsync(10);

            _mockRepo.Verify(r => r.DeleteByIdAsync(10), Times.Once);
        }
    }
}
