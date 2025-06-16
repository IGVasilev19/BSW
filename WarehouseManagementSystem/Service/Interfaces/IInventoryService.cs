using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IInventoryService : Service<Inventory>
    {
        public Task AddNewProductTransactionAsync(Product product, Inventory inventory);
        public Task<IEnumerable<Inventory>> GetAllAsync(int warehouseId);
        public Task AddStockAsync(Inventory inventory);
    }
}
