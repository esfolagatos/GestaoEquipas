using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class GamesWindow : Window
    {
        private readonly GameService _service = new GameService();
        private readonly PlayerService _playerService = new PlayerService();
        private readonly PerformanceService _perfService = new PerformanceService();

        private List<PerformanceRow> _rows = new List<PerformanceRow>();

        public GamesWindow()
        {
            InitializeComponent();
            LoadGames();
            LoadPlayers();
        }

        private void LoadGames()
        {
            GamesList.Items.Clear();
            foreach (var g in _service.GetGames())
            {
                GamesList.Items.Add($"{g.Date:yyyy-MM-dd} vs {g.Opponent} - {g.Result}");
            }
        }

        private void LoadPlayers()
        {
            _rows = new List<PerformanceRow>();
            foreach (var p in _playerService.GetPlayers())
            {
                _rows.Add(new PerformanceRow { PlayerId = p.Id, PlayerName = p.Name });
            }
            PerformanceGrid.ItemsSource = _rows;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var game = new Game
            {
                Date = DatePicker.SelectedDate ?? DateTime.Now,
                Opponent = OpponentBox.Text,
                Result = ResultBox.Text
            };
            int gameId = _service.AddGame(game);

            var stats = new List<PerformanceStat>();
            foreach (var row in _rows)
            {
                stats.Add(new PerformanceStat
                {
                    GameId = gameId,
                    PlayerId = row.PlayerId,
                    Rating = row.Rating
                });
            }
            _perfService.AddStats(stats);

            LoadGames();
            OpponentBox.Text = "";
            ResultBox.Text = "";
            LoadPlayers();
        }

        private class PerformanceRow
        {
            public int PlayerId { get; set; }
            public string PlayerName { get; set; } = string.Empty;
            public int Rating { get; set; }
        }
    }
}
