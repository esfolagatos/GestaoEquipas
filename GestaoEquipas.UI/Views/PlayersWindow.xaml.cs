using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class PlayersWindow : Window
    {
        private readonly PlayerService _service = new PlayerService();

        public PlayersWindow()
        {
            InitializeComponent();
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            PlayersList.Items.Clear();
            foreach (var p in _service.GetPlayers())
            {
                PlayersList.Items.Add($"{p.Name} - {p.Position}");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var player = new Player
            {
                Name = NameBox.Text,
                Position = PositionBox.Text,
                BirthDate = DateTime.Now
            };
            _service.AddPlayer(player);
            LoadPlayers();
            NameBox.Text = "";
            PositionBox.Text = "";
        }
    }
}
