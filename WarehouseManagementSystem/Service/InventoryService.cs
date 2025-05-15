using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class InventoryService : IInventoryService
    {
        public Task CreateAsync(Inventory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inventory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Inventory> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
