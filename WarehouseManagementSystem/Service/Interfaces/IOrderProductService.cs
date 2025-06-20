using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOrderProductService : Service<OrderProduct>
    {
        public decimal CalculateTotalPrice(decimal unitPrice, int quantity, string strategyKey);
    }
}
