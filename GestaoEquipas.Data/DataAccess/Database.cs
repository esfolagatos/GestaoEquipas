using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace GestaoEquipas.Data.DataAccess
{
    public static class Database
    {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gestao_equipas.db");

        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection($"Data Source={DbPath}");
            conn.Open();
            Initialize(conn);
            return conn;
        }

        private static void Initialize(SqliteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS Players(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Position TEXT,
    BirthDate TEXT
);
CREATE TABLE IF NOT EXISTS TrainingSessions(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Date TEXT,
    Notes TEXT
);
CREATE TABLE IF NOT EXISTS Games(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Date TEXT,
    Opponent TEXT,
    Result TEXT
);
";
            cmd.ExecuteNonQuery();
        }
    }
}
