using System;

namespace GestaoEquipas.Data.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Opponent { get; set; } = string.Empty;
        public string Competition { get; set; } = "Liga";
        public string Result { get; set; } = string.Empty; // e.g., "2-1"

        public bool TryGetGoals(out int goalsFor, out int goalsAgainst)
        {
            goalsFor = 0;
            goalsAgainst = 0;

            var parts = Result.Split('-', StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                return false;
            }

            return int.TryParse(parts[0], out goalsFor) && int.TryParse(parts[1], out goalsAgainst);
        }
    }
}
