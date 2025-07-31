using System;

namespace GestaoEquipas.Data.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Archived { get; set; }
    }
}
