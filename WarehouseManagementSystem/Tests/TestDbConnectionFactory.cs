using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public static class TestDbConnectionFactory
{
    public static SqlConnection CreateOpenConnection()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<TestSecretsAnchor>() 
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("Connection string is null or empty. Check secrets setup.");

        var connection = new SqlConnection(connectionString);
        connection.Open(); 

        return connection; 
    }
}
