using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class ExerciseRepository
    {
        public int Add(Exercise exercise)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Exercises(Name, Description, Archived) VALUES(@n,@d,@a)";
            cmd.Parameters.AddWithValue("@n", exercise.Name);
            cmd.Parameters.AddWithValue("@d", exercise.Description);
            cmd.Parameters.AddWithValue("@a", exercise.Archived ? 1 : 0);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT last_insert_rowid()";
            return (int)(long)cmd.ExecuteScalar();
        }

        public IEnumerable<Exercise> GetAll(bool includeArchived = false)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Description, Archived FROM Exercises" + (includeArchived ? string.Empty : " WHERE Archived=0");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Exercise
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Archived = reader.GetInt32(3) == 1
                };
            }
        }

        public void UpdateArchiveStatus(int id, bool archived)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Exercises SET Archived=@a WHERE Id=@id";
            cmd.Parameters.AddWithValue("@a", archived ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
