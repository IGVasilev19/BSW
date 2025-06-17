using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _provider;

        public PricingStrategyFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IPricingStrategy GetStrategy(string key)
        {
            return key switch
            {
                PricingStrategyKeys.OnSale => _provider.GetRequiredService<OnSalePricingStrategy>(),
                PricingStrategyKeys.Standard => _provider.GetRequiredService<StandardPricingStrategy>(),
                _ => throw new ArgumentException($"Unknown strategy: {key}")
            };
        }
    }

}
