using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class PlayerService
    {
        private readonly PlayerRepository _repo = new PlayerRepository();

        public void AddPlayer(Player player) => _repo.Add(player);

        public IEnumerable<Player> GetPlayers() => _repo.GetAll();
    }
}
