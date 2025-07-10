using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class PerformanceService
    {
        private readonly PerformanceRepository _repo = new PerformanceRepository();

        public void AddStats(IEnumerable<PerformanceStat> stats) => _repo.AddStats(stats);

        public IEnumerable<PerformanceStat> GetByGame(int gameId) => _repo.GetByGame(gameId);
    }
}
