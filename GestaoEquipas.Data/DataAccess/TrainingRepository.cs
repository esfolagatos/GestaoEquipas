using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class TrainingRepository
    {
        public void Add(TrainingSession session)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO TrainingSessions(Date, Notes) VALUES(@date, @notes)";
            cmd.Parameters.AddWithValue("@date", session.Date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@notes", session.Notes);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<TrainingSession> GetAll()
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Date, Notes FROM TrainingSessions";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new TrainingSession
                {
                    Id = reader.GetInt32(0),
                    Date = System.DateTime.Parse(reader.GetString(1)),
                    Notes = reader.GetString(2)
                };
            }
        }
    }
}
