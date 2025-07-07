using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class TrainingService
    {
        private readonly TrainingRepository _repo = new TrainingRepository();

        public void AddSession(TrainingSession session) => _repo.Add(session);

        public IEnumerable<TrainingSession> GetSessions() => _repo.GetAll();
    }
}
