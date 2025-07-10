using System;
using System.Collections.Generic;

namespace GestaoEquipas.Data.Models
{
    public class TrainingSheet
    {
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<Exercise> Exercises { get; set; } = new();
    }
}
