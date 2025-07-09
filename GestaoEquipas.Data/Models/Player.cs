using System;

namespace GestaoEquipas.Data.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
