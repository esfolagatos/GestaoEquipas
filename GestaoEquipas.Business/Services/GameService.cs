using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class GameService
    {
        private readonly GameRepository _repo = new GameRepository();

        public void AddGame(Game game) => _repo.Add(game);

        public IEnumerable<Game> GetGames() => _repo.GetAll();
    }
}
