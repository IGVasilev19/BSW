using Domain;
using Service.Interfaces;
using Service.Strategies.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderProductService : IOrderProductService
    {
        private readonly PricingStrategyFactory _strategyFactory;

        public OrderProductService (PricingStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        public decimal CalculateTotalPrice(decimal price, int quantity, string pricingStrategyKey)
        {
            var strategy = _strategyFactory.GetStrategy(pricingStrategyKey);
            return strategy.CalculatePrice(price, quantity);
        }

        public Task CreateAsync(OrderProduct entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderProduct>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderProduct> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
