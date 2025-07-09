using GestaoEquipas.Data.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace GestaoEquipas.Data.DataAccess
{
    public class PlayerRepository
    {
        public void Add(Player player)
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Players(Name, Position, BirthDate) VALUES(@name, @pos, @birth)";
            cmd.Parameters.AddWithValue("@name", player.Name);
            cmd.Parameters.AddWithValue("@pos", player.Position);
            cmd.Parameters.AddWithValue("@birth", player.BirthDate.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Player> GetAll()
        {
            using var conn = Database.GetConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Position, BirthDate FROM Players";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Player
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Position = reader.GetString(2),
                    BirthDate = System.DateTime.Parse(reader.GetString(3))
                };
            }
        }
    }
}
