using System;

namespace GestaoEquipas.Data.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int TrainingSessionId { get; set; }
        public int PlayerId { get; set; }
        public bool Present { get; set; }
    }
}
