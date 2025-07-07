using System;

namespace GestaoEquipas.Data.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
