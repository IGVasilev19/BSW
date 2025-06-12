using DAL.Interfaces;
using DAL.Utility;
using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly DbHelper _db;

        public WarehouseRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Warehouse warehouse)
        {
            using var conn = _db.GetConnection();
            conn.OpenAsync();

            var cmd = _db.CreateCommand(@"INSERT INTO Warehouse (Name) VALUES (@Name, @AddressId)", conn);

            cmd.Parameters.AddWithValue("@Name", warehouse.Name);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> AddAsync(Warehouse warehouse, SqlConnection connection, SqlTransaction transaction)
        {
            using var conn = _db.GetConnection();
            conn.OpenAsync();

            var cmd = _db.CreateCommand(@"INSERT INTO Warehouse (Name,AddressId) VALUES (@Name,@AddressId); SELECT SCOPE_IDENTITY();", connection, transaction);

            cmd.Parameters.AddWithValue("@Name", warehouse.Name);
            cmd.Parameters.AddWithValue("@AddressId", warehouse.AddressId);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            var list = new List<Warehouse>();

            using var conn = _db.GetConnection();
            conn.Open();
            var cmd = _db.CreateCommand("SELECT * FROM Warehouse", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Warehouse(
                    (int)reader["WarehouseId"],
                    reader["Name"].ToString(),
                    (int)reader["AddressId"]
                ));
            }
            return list;
        }

        public async Task<Warehouse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync (Warehouse warehouse)
        {
            throw new NotImplementedException();
        }
    }
}
