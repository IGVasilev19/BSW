using DAL.Interfaces;
using DAL.Utility;
using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbHelper _db;

        public CategoryRepository (DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Category category)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            try
            {
                var cmd = _db.CreateCommand(@"INSERT INTO Category (Name,WarehouseId) VALUES (@Name, @WarehouseId)", conn);

                cmd.Parameters.AddWithValue("@Name", category.Name);
                cmd.Parameters.AddWithValue("@WarehouseId", category.WarehouseId);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new QueryFailedException("This category already exists", ex);
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var list = new List<Category>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Category", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Category(
                    (int)reader["CategoryId"],
                    reader["Name"].ToString()
                ));
            }
            return list;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int warehouseId)
        {
            var list = new List<Category>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Category WHERE WarehouseId = @WarehouseId", conn);

            cmd.Parameters.AddWithValue("@WarehouseId", warehouseId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Category(
                    (int)reader["CategoryId"],
                    reader["Name"].ToString(),
                    (int)reader["WarehouseId"]
                ));
            }
            return list;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand("SELECT * FROM Category WHERE CategoryId = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Category(
                    (int)reader["CategoryId"],
                    reader["Name"].ToString(),
                    (int)reader["WarehouseId"]
                );
            }

            return null;
        }

        public Task UpdateAsync(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
