namespace PAXAnalyticsAPI.Utility
{
    public class DatabaseConnectionStringBuilder
    {
        public static string GetSqlConnectionString(IConfiguration config)
        {
            return $"Host={config["PAXADatabaseConnection_Server"]};Username={config["PAXADatabaseConnection_Username"]};Password={config["PAXADatabaseConnection_Password"]};Database={config["PAXADatabaseConnection_Database"]}"; 
        }
    }
}
