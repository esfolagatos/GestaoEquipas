using System;

namespace GestaoEquipas.Data.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Opponent { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // e.g., "2-1"
    }
}
