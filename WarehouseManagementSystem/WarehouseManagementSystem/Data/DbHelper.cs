using Microsoft.Data.SqlClient;
using System.Data;

namespace WarehouseManagementSystem.Data
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlCommand CreateCommand(string query, SqlConnection connection)
        {
            return new SqlCommand(query, connection);
        }
    }
}