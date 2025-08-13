namespace Backend
{
    public static class EnvironmentVariables
    {
        public static string SqliteConnectionString = Environment.GetEnvironmentVariable("SQLITE_CONNSTR") ?? throw new Exception("You must provide a sqlite connection string");
    }

}