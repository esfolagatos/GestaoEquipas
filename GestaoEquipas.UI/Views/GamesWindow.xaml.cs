using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class GamesWindow : Window
    {
        private readonly GameService _service = new GameService();

        public GamesWindow()
        {
            InitializeComponent();
            LoadGames();
        }

        private void LoadGames()
        {
            GamesList.Items.Clear();
            foreach (var g in _service.GetGames())
            {
                GamesList.Items.Add($"{g.Date:yyyy-MM-dd} vs {g.Opponent} - {g.Result}");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var game = new Game
            {
                Date = DatePicker.SelectedDate ?? DateTime.Now,
                Opponent = OpponentBox.Text,
                Result = ResultBox.Text
            };
            _service.AddGame(game);
            LoadGames();
            OpponentBox.Text = "";
            ResultBox.Text = "";
        }
    }
}
