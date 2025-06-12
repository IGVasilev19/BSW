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
    public class ZoneRepository : IZoneRepository
    {
        private readonly DbHelper _db;

        public ZoneRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Zone zone)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            try
            {
                var cmd = _db.CreateCommand(@"INSERT INTO Zone (Name, Capacity, WarehouseId) VALUES (@Name, @Capacity, @WarehouseId)", conn);

                cmd.Parameters.AddWithValue("@Name", zone.Name);
                cmd.Parameters.AddWithValue("@Capacity", zone.Capacity);
                cmd.Parameters.AddWithValue("@WarehouseId", zone.WarehouseId);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new QueryFailedException("This zone already exists", ex);
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zone>> GetAllAsync()
        {
            var list = new List<Zone>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Zone", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Zone(
                    (int)reader["ZoneId"],
                    reader["Name"].ToString(),
                    (decimal)reader["Capacity"],
                    (int)reader["WarehouseId"]
                ));
            }
            return list;
        }

        public async Task<IEnumerable<Zone>> GetAllAsync(int warehouseId)
        {
            var list = new List<Zone>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Zone WHERE WarehouseId = @WarehouseId", conn);

            cmd.Parameters.AddWithValue("@WarehouseId", warehouseId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Zone(
                    (int)reader["ZoneId"],
                    reader["Name"].ToString(),
                    (decimal)reader["Capacity"],
                    (int)reader["WarehouseId"]
                ));
            }
            return list;
        }

        public async Task<Zone> GetByIdAsync(int id)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var cmd = _db.CreateCommand("SELECT * FROM Zone WHERE ZoneId = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Zone(
                    (int)reader["ZoneId"],
                    reader["Name"].ToString(),
                    (decimal)reader["Capacity"],
                    (int)reader["WarehouseId"]
                );
            }

            return null;
        }

        public Task UpdateAsync(Zone obj)
        {
            throw new NotImplementedException();
        }
    }
}
