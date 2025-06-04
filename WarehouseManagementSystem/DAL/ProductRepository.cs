using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbHelper _db;

        public ProductRepository (DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Product product)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            try
            {
                var cmd = _db.CreateCommand(@"INSERT INTO [Product] (Name, Price, CategoryId) VALUES (@Name, @Price, @CategoryId)", conn);

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new QueryFailedException("This product already exists", ex);
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var list = new List<Product>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Product p LEFT JOIN Inventory i ON p.ProductId = i.ProductId WHERE i.ProductId IS NULL;", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Product(
                    (int)reader["ProductId"],
                    reader["Name"].ToString(),
                    (decimal)reader["Price"],
                    (int)reader["CategoryId"]
                ));
            }
            return list;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
