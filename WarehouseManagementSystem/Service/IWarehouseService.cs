using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IWarehouseService : Service<Warehouse>
    {
        Task<int> CreateAsync(Warehouse warehouse, SqlConnection conn, SqlTransaction tran);
    }
}
