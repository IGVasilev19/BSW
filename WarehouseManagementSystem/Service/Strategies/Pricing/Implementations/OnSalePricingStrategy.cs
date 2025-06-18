using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Strategies.Pricing.Implementations
{
    public class OnSalePricingStrategy : IPricingStrategy
    {
        public string Key => PricingStrategyKeys.OnSale;

        public decimal CalculatePrice(decimal price, int quantity)
        {

            return price * quantity * 0.8m;
        }
    }
}
