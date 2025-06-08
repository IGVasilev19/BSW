using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IInventoryRepository : Repository<Inventory>
    {
        public Task AddNewProductTransactionAsync(Product product, Inventory inventory);
        public Task AddAsync(Inventory inventory, SqlConnection connection, SqlTransaction transaction);
    }
}
