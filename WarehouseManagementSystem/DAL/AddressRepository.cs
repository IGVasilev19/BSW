using BLL;
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

        public void Add(Address address)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = _db.CreateCommand(@"INSERT INTO Address (Country, City, StreetName, StreetNumber, Zip) VALUES (@Country, @City, @StreetName, @StreetNumber, @Zip)", conn);

            cmd.Parameters.AddWithValue("@Country", address.Country);
            cmd.Parameters.AddWithValue("@City", address.City);
            cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
            cmd.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
            cmd.Parameters.AddWithValue("@Zip", address.Zip);

            cmd.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> GetAll()
        {
            var list = new List<Address>();

            using var conn = _db.GetConnection();
            conn.Open();
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

        public Address GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
