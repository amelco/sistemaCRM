using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace sistema.Repositories
{
    public static class RawSql
    {
        public static string connectionString = EnvironmentVariables.SqliteConnectionString;

        // TODO (Andre): trocar asserts por error handling
        public static T? Query<T>(string query, Func<SqliteDataReader, T> map)
        {
            var conn = new SqliteConnection(connectionString);
            Debug.Assert(conn is not null);
            conn.Open();

            Console.WriteLine(query);
            var cmd = new SqliteCommand(query, conn);
            Console.WriteLine(cmd.ToString());
            Debug.Assert(cmd is not null);

            var reader = cmd.ExecuteReader();
            if (reader is null)
            {
                conn.Close();
                return default;
            }

            reader.Read();
            var entidade = map(reader);
            
            conn.Close();
            return entidade;
        }

        // retorna o numero de registros afetados pela consulta
        public static int NonQuery(string query)
        {
            var conn = new SqliteConnection(connectionString);
            Debug.Assert(conn is not null);
            conn.Open();

            var cmd = new SqliteCommand(query, conn);
            Debug.Assert(cmd is not null);

            var qtd = cmd.ExecuteNonQuery();
            conn.Close();

            return qtd;
        }
    }
}