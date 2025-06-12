using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Strategies.Pricing
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(Product product);
    }
}
