using Microsoft.Data.SqlClient;

namespace WebAppDb.Services.DBHelper
{
    public static class DButil
    {

        public static SqlConnection GetConnection()
        {
            SqlConnection? connection;
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            string? url = configuration.GetConnectionString("DefaultConnection");

            try
            {
                connection = new SqlConnection(url);
                return connection;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
