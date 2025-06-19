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
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        private ProductService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCallRepoAdd()
        {
            var product = new Product("Laptop", 12.99m, 1);

            await _service.CreateAsync(product);

            _mockRepo.Verify(r => r.AddAsync(product), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldThrow_WhenRepoThrows()
        {
            var product = new Product("Item", 10.0m, categoryId: 1);
            _mockRepo
                .Setup(r => r.AddAsync(product))
                .ThrowsAsync(new QueryFailedException("Simulated failure"));

            await Assert.ThrowsExceptionAsync<QueryFailedException>(() =>
                _service.CreateAsync(product));
        }


        [TestMethod]
        public async Task CreateAsync_WithTransaction_ShouldReturnInsertedId()
        {
            using var conn = TestDbConnectionFactory.CreateOpenConnection();
            using var transaction = conn.BeginTransaction();

            var product = new Product("Product A", 9.99m, categoryId: 1);

            _mockRepo.Setup(r => r.AddAsync(product, conn, transaction))
                     .ReturnsAsync(42);

            var result = await _service.CreateAsync(product, conn, transaction);

            Assert.AreEqual(42, result);
        }



        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new Product("P1", 10m, 1),
                new Product("P2", 20m, 2),
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.GetAllAsync();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectProduct()
        {
            var product = new Product(1, "Phone", 49.99m, 5);
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            var result = await _service.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Phone", result.Name);
        }

    }
}
