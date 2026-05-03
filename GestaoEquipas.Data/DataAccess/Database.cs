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
    Competition TEXT,
    Result TEXT
);
CREATE TABLE IF NOT EXISTS Competitions(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Type TEXT NOT NULL,
    Season TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS AttendanceRecords(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    TrainingSessionId INTEGER,
    PlayerId INTEGER,
    Present INTEGER
);
CREATE TABLE IF NOT EXISTS PerformanceStats(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    GameId INTEGER,
    PlayerId INTEGER,
    Rating INTEGER
);
CREATE TABLE IF NOT EXISTS Exercises(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Description TEXT,
    Archived INTEGER
);
";
            cmd.ExecuteNonQuery();

            var migration = conn.CreateCommand();
            migration.CommandText = "ALTER TABLE Games ADD COLUMN Competition TEXT DEFAULT 'Liga'";
            try
            {
                migration.ExecuteNonQuery();
            }
            catch (SqliteException)
            {
                // coluna já existe
            }

            var seed = conn.CreateCommand();
            seed.CommandText = @"
INSERT INTO Competitions(Name, Type, Season)
SELECT 'Liga', 'Liga', '2025/2026' WHERE NOT EXISTS (SELECT 1 FROM Competitions);
INSERT INTO Competitions(Name, Type, Season)
SELECT 'Taça', 'Taça', '2025/2026' WHERE NOT EXISTS (SELECT 1 FROM Competitions WHERE Name='Taça' AND Season='2025/2026');
";
            seed.ExecuteNonQuery();
        }
    }
}
