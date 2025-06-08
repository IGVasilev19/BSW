using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProductRepository : Repository<Product>
    {
        public Task<int> AddAsync(Product product, SqlConnection connection, SqlTransaction transaction);
    }
}
