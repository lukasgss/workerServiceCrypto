using Microsoft.Data.SqlClient;

namespace WorkerService;

public static class DatabaseInitializer
{
	public static void InitializeDatabase(IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("DefaultConnection");

		SqlConnectionStringBuilder builder = new(connectionString);
		string? database = builder.InitialCatalog;
		builder.InitialCatalog = "master";
		builder.Encrypt = true;
		builder.TrustServerCertificate = true;

		using SqlConnection connection = new(builder.ConnectionString);
		connection.Open();

		using SqlCommand command = connection.CreateCommand();
		command.CommandText = $@"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{database}')
                    BEGIN
                        CREATE DATABASE [{database}]
                    END";
		command.ExecuteNonQuery();
	}
}