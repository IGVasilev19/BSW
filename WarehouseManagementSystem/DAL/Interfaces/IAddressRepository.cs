using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAddressRepository : Repository<Address>
    {
        public Task<int> AddAsync(Address address, SqlConnection connection, SqlTransaction transaction);
    }
}
