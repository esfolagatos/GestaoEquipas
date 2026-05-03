using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class CompetitionService
    {
        private readonly CompetitionRepository _repo = new CompetitionRepository();

        public int AddCompetition(Competition competition) => _repo.Add(competition);

        public IEnumerable<Competition> GetCompetitions() => _repo.GetAll();
    }
}
