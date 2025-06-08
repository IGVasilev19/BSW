using Domain;
using Exceptions;
using Microsoft.Data.SqlClient;
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
        private readonly IProductRepository _productRepository;

        public InventoryRepository(DbHelper db, IProductRepository productRepository)
        {
            _db = db;
            _productRepository = productRepository;
        }

        public async Task AddAsync(Inventory inventory)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand(@"INSERT INTO Inventory (ProductId, ZoneId, Quantity, LastUpdate) VALUES (@ProductId, @ZoneId, @Quantity, @LastUpdate)", conn);

            cmd.Parameters.AddWithValue("@ProductId", inventory.ProductId);
            cmd.Parameters.AddWithValue("@ZoneId", inventory.ZoneId);
            cmd.Parameters.AddWithValue("@Quantity", inventory.Quantity);
            cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task AddAsync(Inventory inventory, SqlConnection connection, SqlTransaction transaction)
        {
            var cmd = _db.CreateCommand(@"INSERT INTO Inventory (ProductId, ZoneId, Quantity, LastUpdate) VALUES (@ProductId, @ZoneId, @Quantity, @LastUpdate); SELECT SCOPE_IDENTITY();", connection, transaction);

            cmd.Parameters.AddWithValue("@ProductId", inventory.ProductId);
            cmd.Parameters.AddWithValue("@ZoneId", inventory.ZoneId);
            cmd.Parameters.AddWithValue("@Quantity", 0);
            cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);

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

        public async Task AddNewProductTransactionAsync(Product product, Inventory inventory)
        {
            using var connection = _db.GetConnection();
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                var productId = await _productRepository.AddAsync(product, connection, transaction);

                inventory = new Inventory(productId, inventory.ZoneId);

                await AddAsync(inventory, connection, transaction);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new QueryFailedException("Failed to add a new product to the system", ex);
            }
        }
    }
}
