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
            cmd.CommandText = "INSERT INTO Games(Date, Opponent, Result) VALUES(@date, @opp, @res)";
            cmd.Parameters.AddWithValue("@date", game.Date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@opp", game.Opponent);
            cmd.Parameters.AddWithValue("@res", game.Result);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT last_insert_rowid()";
            return (int)(long)cmd.ExecuteScalar();
        }

        public IEnumerable<Game> GetAll()
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Date, Opponent, Result FROM Games";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Game
                {
                    Id = reader.GetInt32(0),
                    Date = System.DateTime.Parse(reader.GetString(1)),
                    Opponent = reader.GetString(2),
                    Result = reader.GetString(3)
                };
            }
        }
    }
}
