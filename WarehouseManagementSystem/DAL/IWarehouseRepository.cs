using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IWarehouseRepository : Repository<Warehouse>
    {
        public Task<int> AddAsync (Warehouse warehouse, SqlConnection conn, SqlTransaction tran);
    }
}
