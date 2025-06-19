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
    public class AddressServiceTests
    {
        private Mock<IAddressRepository> _mockRepo;
        private AddressService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IAddressRepository>();
            _service = new AddressService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllAddresses()
        {
            var addresses = new List<Address>
        {
            new Address(1, "NL", "Eindhoven", "Fontysstraat", 7, 5612),
            new Address(2, "DE", "Berlin", "Hauptstraße", 12, 10115)
        };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(addresses);

            var result = await _service.GetAllAsync();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectAddress()
        {
            var address = new Address(1, "NL", "Eindhoven", "Fontysstraat", 7, 5612);

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(address);

            var result = await _service.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Eindhoven", result.City);
            Assert.AreEqual(5612, result.Zip);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNull_IfNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Address)null);

            var result = await _service.GetByIdAsync(99);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task CreateAsync_CallsRepo()
        {
            var address = new Address("NL", "Eindhoven", "Fontysstraat", 7, 5612);

            await _service.CreateAsync(address);

            _mockRepo.Verify(r => r.AddAsync(address), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_CallsRepo()
        {
            var address = new Address(1, "NL", "Eindhoven", "Fontysstraat", 7, 5612);

            await _service.UpdateAsync(address);

            _mockRepo.Verify(r => r.UpdateAsync(address), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_CallsRepo()
        {
            await _service.DeleteByIdAsync(1);

            _mockRepo.Verify(r => r.DeleteByIdAsync(1), Times.Once);
        }
    }
}
