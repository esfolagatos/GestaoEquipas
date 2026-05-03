using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class GameRepository
    {
        public int Add(Game game)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Games(Date, Opponent, Competition, Result) VALUES(@date, @opp, @comp, @res)";
            cmd.Parameters.AddWithValue("@date", game.Date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@opp", game.Opponent);
            cmd.Parameters.AddWithValue("@comp", game.Competition);
            cmd.Parameters.AddWithValue("@res", game.Result);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT last_insert_rowid()";
            return (int)(long)cmd.ExecuteScalar();
        }

        public IEnumerable<Game> GetAll()
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Date, Opponent, Competition, Result FROM Games ORDER BY Date DESC";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Game
                {
                    Id = reader.GetInt32(0),
                    Date = System.DateTime.Parse(reader.GetString(1)),
                    Opponent = reader.GetString(2),
                    Competition = reader.IsDBNull(3) ? "Liga" : reader.GetString(3),
                    Result = reader.GetString(4)
                };
            }
        }

        public void Delete(int gameId)
        {
            using var conn = Database.GetConnection();

            var deleteStats = conn.CreateCommand();
            deleteStats.CommandText = "DELETE FROM PerformanceStats WHERE GameId=@gid";
            deleteStats.Parameters.AddWithValue("@gid", gameId);
            deleteStats.ExecuteNonQuery();

            var deleteGame = conn.CreateCommand();
            deleteGame.CommandText = "DELETE FROM Games WHERE Id=@id";
            deleteGame.Parameters.AddWithValue("@id", gameId);
            deleteGame.ExecuteNonQuery();
        }
    }
}
