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
        public decimal CalculatePrice(Product product)
        {
            return product.Price * 0.8m;
        }
    }
}
