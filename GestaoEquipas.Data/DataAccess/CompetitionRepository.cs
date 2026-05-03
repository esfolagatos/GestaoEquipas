using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class CompetitionRepository
    {
        public int Add(Competition competition)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Competitions(Name, Type, Season) VALUES(@name, @type, @season)";
            cmd.Parameters.AddWithValue("@name", competition.Name);
            cmd.Parameters.AddWithValue("@type", competition.Type);
            cmd.Parameters.AddWithValue("@season", competition.Season);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT last_insert_rowid()";
            return (int)(long)cmd.ExecuteScalar();
        }

        public IEnumerable<Competition> GetAll()
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Type, Season FROM Competitions ORDER BY Season DESC, Name";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Competition
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Type = reader.GetString(2),
                    Season = reader.GetString(3)
                };
            }
        }
    }
}
