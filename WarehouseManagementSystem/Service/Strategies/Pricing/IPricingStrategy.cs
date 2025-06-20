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
        public string Key { get; }
        public decimal CalculatePrice(decimal price, int quantity);
    }
}
