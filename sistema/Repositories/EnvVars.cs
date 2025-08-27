namespace sistema.Repositories
{
    public static class EnvironmentVariables
    {     
        public static readonly string SqliteConnectionString = Environment.GetEnvironmentVariable("SQLITE_CONNSTR") ?? "Data Source=database.db";
    }
}