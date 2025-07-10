using System;

namespace GestaoEquipas.Data.Models
{
    public class PerformanceStat
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int Rating { get; set; }
    }
}
