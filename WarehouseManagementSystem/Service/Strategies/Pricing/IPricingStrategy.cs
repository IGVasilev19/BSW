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
        string Key { get; }
        decimal CalculatePrice(decimal price, int quantity);
    }
}
