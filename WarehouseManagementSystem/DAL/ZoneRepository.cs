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

        public Task<IEnumerable<Zone>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Zone> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Zone obj)
        {
            throw new NotImplementedException();
        }
    }
}
