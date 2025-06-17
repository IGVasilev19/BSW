using DAL.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderProductRepository : IOrderProductRepository
    {
        public Task AddAsync(OrderProduct obj)
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

        public Task UpdateAsync(OrderProduct obj)
        {
            throw new NotImplementedException();
        }
    }
}
