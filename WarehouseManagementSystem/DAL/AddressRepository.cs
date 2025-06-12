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
    public class AddressRepository : IAddressRepository
    {
        private readonly DbHelper _db;

        public AddressRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task AddAsync(Address address)
        {
            using var conn = _db.GetConnection();
            conn.OpenAsync();

            var cmd = _db.CreateCommand(@"INSERT INTO Address (Country, City, StreetName, StreetNumber, Zip) VALUES (@Country, @City, @StreetName, @StreetNumber, @Zip)", conn);

            cmd.Parameters.AddWithValue("@Country", address.Country);
            cmd.Parameters.AddWithValue("@City", address.City);
            cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
            cmd.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
            cmd.Parameters.AddWithValue("@Zip", address.Zip);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> AddAsync(Address address, SqlConnection connection, SqlTransaction transaction)
        {
            var cmd = _db.CreateCommand(@"INSERT INTO Address (Country, City, StreetName, StreetNumber, Zip) VALUES (@Country, @City, @StreetName, @StreetNumber, @Zip); SELECT SCOPE_IDENTITY();", connection, transaction);

            cmd.Parameters.AddWithValue("@Country", address.Country);
            cmd.Parameters.AddWithValue("@City", address.City);
            cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
            cmd.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
            cmd.Parameters.AddWithValue("@Zip", address.Zip);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var list = new List<Address>();

            using var conn = _db.GetConnection();
            conn.OpenAsync();
            var cmd = _db.CreateCommand("SELECT * FROM Address", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Address(
                    (int)reader["AddressId"],
                    reader["Country"].ToString(),
                    reader["City"].ToString(),
                    reader["StreetName"].ToString(),
                    (int)reader["StreetNumber"],
                    (int)reader["Zip"]
                ));
            }
            return list;
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
