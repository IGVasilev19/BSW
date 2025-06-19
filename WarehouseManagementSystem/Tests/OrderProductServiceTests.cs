using DAL.Interfaces;
using Domain;
using Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using Service.Interfaces;
using Service.Strategies.Pricing;
using Service.Strategies.Pricing.Implementations;
using Service.Utility;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class OrderProductServiceTests
    {
        private Mock<PricingStrategyFactory> _mockFactory;
        private Mock<IPricingStrategy> _mockStrategy;
        private OrderProductService _service;

        [TestInitialize]
        public void Init()
        {
            _mockFactory = new Mock<PricingStrategyFactory>(MockBehavior.Strict, null!, null!);
            _mockStrategy = new Mock<IPricingStrategy>();
            _service = new OrderProductService(_mockFactory.Object);
        }

        [TestMethod]
        public void CalculateTotalPrice_ShouldReturnExpectedTotal()
        {
            var strategyMock = new Mock<IPricingStrategy>();
            strategyMock.SetupGet(s => s.Key).Returns(PricingStrategyKeys.OnSale);
            strategyMock.Setup(s => s.CalculatePrice(100, 2)).Returns(160); 

            var strategies = new List<IPricingStrategy> { strategyMock.Object };

            var loggerMock = new Mock<ILogger<PricingStrategyFactory>>();
            var factory = new PricingStrategyFactory(strategies, loggerMock.Object);

            var service = new OrderProductService(factory);

            decimal result = service.CalculateTotalPrice(100, 2, PricingStrategyKeys.OnSale);

            Assert.AreEqual(160, result);
        }

        [TestMethod]
        public void CalculateTotalPrice_ShouldThrow_WhenStrategyKeyInvalid()
        {
            var price = 10m;
            var quantity = 1;
            var key = "InvalidKey"; 

            var strategies = new List<IPricingStrategy>
            {
                new StandardPricingStrategy(),
                new OnSalePricingStrategy()   
            };

            var loggerMock = new Mock<ILogger<PricingStrategyFactory>>();
            var factory = new PricingStrategyFactory(strategies, loggerMock.Object);
            var service = new OrderProductService(factory);

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                service.CalculateTotalPrice(price, quantity, key));

            Assert.AreEqual("Unknown strategy: InvalidKey", ex.Message);
        }


    }
}
