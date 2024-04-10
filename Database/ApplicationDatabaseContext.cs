using Microsoft.Data.SqlClient;

namespace UBB_SE_2024_Gaborment.Database
{
    public class ApplicationDatabaseContext 
    {
        private readonly string _connectionString;

        public ApplicationDatabaseContext()
        {
            _connectionString = $"Data Source=tcp:{Environment.GetEnvironmentVariable("DB_IP")},1433;User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
                                $"Password={Environment.GetEnvironmentVariable("DB_PASS")};Initial Catalog={Environment.GetEnvironmentVariable("DB_SCHEMA")};TrustServerCertificate=True"
;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

