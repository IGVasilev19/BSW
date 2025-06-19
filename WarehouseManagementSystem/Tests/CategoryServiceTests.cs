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
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _mockRepo;
        private CategoryService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<ICategoryRepository>();
            _service = new CategoryService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task CreateAsync_CallsRepoSuccessfully()
        {
            var category = new Category("Electronics");

            await _service.CreateAsync(category);

            _mockRepo.Verify(r => r.AddAsync(category), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsync_ThrowsQueryFailedException()
        {
            var category = new Category("Food");
            _mockRepo.Setup(r => r.AddAsync(category)).ThrowsAsync(new QueryFailedException("fail"));

            await Assert.ThrowsExceptionAsync<QueryFailedException>(() => _service.CreateAsync(category));
        }

        [TestMethod]
        public async Task GetAllAsync_WithWarehouseId_ReturnsResults()
        {
            var expected = new List<Category>
        {
            new Category(1, "Drinks", 1),
            new Category(2, "Snacks", 1)
        };

            _mockRepo.Setup(r => r.GetAllAsync(1)).ReturnsAsync(expected);

            var result = await _service.GetAllAsync(1);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectCategory()
        {
            var category = new Category(1, "Food", 1);
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(category);

            var result = await _service.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.CategoryId);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNull_IfNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null);

            var result = await _service.GetByIdAsync(999);

            Assert.IsNull(result);
        }
    }
}
