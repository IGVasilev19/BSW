using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IAddressService : Service<Address>
    {
        public Task<int> CreateAsync(Address address, SqlConnection conn, SqlTransaction tran);
    }
}
