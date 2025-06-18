using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Strategies.Pricing.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Strategies.Pricing
{
    public class PricingStrategyFactory
    {
        private readonly IEnumerable<IPricingStrategy> _strategies;
        private readonly ILogger<PricingStrategyFactory> _logger;

        public PricingStrategyFactory(IEnumerable<IPricingStrategy> strategies, ILogger<PricingStrategyFactory> logger)
        {
            _strategies = strategies;
            _logger = logger;
        }

        public IPricingStrategy GetStrategy(string key)
        {
            _logger.LogInformation("Fetching pricing strategy for key: {Key}", key);

            var strategy = _strategies.FirstOrDefault(s => s.Key == key);

            if (strategy == null)
            {
                _logger.LogWarning("Unknown pricing strategy requested: {Key}", key);
                throw new ArgumentException($"Unknown strategy: {key}");
            }

            return strategy;
        }
    }
}
