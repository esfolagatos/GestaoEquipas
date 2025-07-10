using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class AttendanceRepository
    {
        public void AddRecords(IEnumerable<AttendanceRecord> records)
        {
            using var conn = Database.GetConnection();
            foreach (var rec in records)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO AttendanceRecords(TrainingSessionId, PlayerId, Present) VALUES(@ts, @pid, @pre)";
                cmd.Parameters.AddWithValue("@ts", rec.TrainingSessionId);
                cmd.Parameters.AddWithValue("@pid", rec.PlayerId);
                cmd.Parameters.AddWithValue("@pre", rec.Present ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<AttendanceRecord> GetBySession(int sessionId)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, TrainingSessionId, PlayerId, Present FROM AttendanceRecords WHERE TrainingSessionId=@ts";
            cmd.Parameters.AddWithValue("@ts", sessionId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new AttendanceRecord
                {
                    Id = reader.GetInt32(0),
                    TrainingSessionId = reader.GetInt32(1),
                    PlayerId = reader.GetInt32(2),
                    Present = reader.GetInt32(3) == 1
                };
            }
        }
    }
}
