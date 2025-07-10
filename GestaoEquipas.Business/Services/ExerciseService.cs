using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class ExerciseService
    {
        private readonly ExerciseRepository _repo = new ExerciseRepository();

        public int AddExercise(Exercise exercise) => _repo.Add(exercise);

        public IEnumerable<Exercise> GetExercises(bool includeArchived = false) => _repo.GetAll(includeArchived);

        public void ArchiveExercise(int id) => _repo.UpdateArchiveStatus(id, true);

        public void UnarchiveExercise(int id) => _repo.UpdateArchiveStatus(id, false);
    }
}
