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
                var cmd = _db.CreateCommand(@"INSERT INTO Category (Name) VALUES (@Name)", conn);

                cmd.Parameters.AddWithValue("@Name", category.Name);

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

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
