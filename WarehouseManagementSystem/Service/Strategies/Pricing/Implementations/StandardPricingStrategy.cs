using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Strategies.Pricing.Implementations
{
    public class StandardPricingStrategy : IPricingStrategy
    {
        public string Key => PricingStrategyKeys.Standard;
        public decimal CalculatePrice(decimal price, int quantity) => price * quantity;
    }
}
