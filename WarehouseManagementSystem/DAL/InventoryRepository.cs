using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DbHelper _db;

        public InventoryRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Inventory inventory)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand(@"INSERT INTO Inventory (ProductId, ZoneId, Quantity, LastUpdate) VALUES (@ProductId, @ZoneId, @Quantity, @LastUpdate)", conn);

            cmd.Parameters.AddWithValue("@ProductId", inventory.ProductId);
            cmd.Parameters.AddWithValue("@ZoneId", inventory.ZoneId);
            cmd.Parameters.AddWithValue("@Quantity", inventory.Quantity);
            cmd.Parameters.AddWithValue("@LastUpdate", inventory.LastUpdate);

            await cmd.ExecuteNonQueryAsync();
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

        public Task UpdateAsync(Inventory obj)
        {
            throw new NotImplementedException();
        }
    }
}
