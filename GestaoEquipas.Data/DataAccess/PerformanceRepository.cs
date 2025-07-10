using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class PerformanceRepository
    {
        public void AddStats(IEnumerable<PerformanceStat> stats)
        {
            using var conn = Database.GetConnection();
            foreach (var st in stats)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO PerformanceStats(GameId, PlayerId, Rating) VALUES(@g, @p, @r)";
                cmd.Parameters.AddWithValue("@g", st.GameId);
                cmd.Parameters.AddWithValue("@p", st.PlayerId);
                cmd.Parameters.AddWithValue("@r", st.Rating);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<PerformanceStat> GetByGame(int gameId)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, GameId, PlayerId, Rating FROM PerformanceStats WHERE GameId=@g";
            cmd.Parameters.AddWithValue("@g", gameId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new PerformanceStat
                {
                    Id = reader.GetInt32(0),
                    GameId = reader.GetInt32(1),
                    PlayerId = reader.GetInt32(2),
                    Rating = reader.GetInt32(3)
                };
            }
        }
    }
}
