using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GestaoEquipas.UI.Views
{
    public partial class GamesWindow : Window
    {
        private readonly GameService _service = new GameService();
        private readonly PlayerService _playerService = new PlayerService();
        private readonly PerformanceService _perfService = new PerformanceService();
        private readonly CompetitionService _competitionService = new CompetitionService();

        private List<PerformanceRow> _rows = new List<PerformanceRow>();
        private List<Game> _games = new List<Game>();

        public GamesWindow()
        {
            InitializeComponent();
            LoadGames();
            LoadPlayers();
            LoadCompetitions();
        }

        private void LoadCompetitions()
        {
            var competitions = _competitionService.GetCompetitions().ToList();
            CompetitionBox.ItemsSource = competitions;
            CompetitionBox.DisplayMemberPath = nameof(Competition.Name);
            CompetitionBox.SelectedValuePath = nameof(Competition.Name);
            if (competitions.Count > 0)
            {
                CompetitionBox.SelectedIndex = 0;
            }

            CompetitionFilterBox.Items.Clear();
            CompetitionFilterBox.Items.Add("Todas");
            foreach (var comp in competitions.Select(c => c.Name).Distinct())
            {
                CompetitionFilterBox.Items.Add(comp);
            }
            CompetitionFilterBox.SelectedIndex = 0;
        }

        private void LoadGames()
        {
            _games = _service.GetGames().ToList();
            ApplyGamesFilter();
            ApplyStandings();
        }

        private void ApplyStandings()
        {
            var selectedFilter = CompetitionFilterBox.SelectedItem?.ToString();
            var standingCompetition = string.IsNullOrWhiteSpace(selectedFilter) || selectedFilter == "Todas"
                ? "Liga"
                : selectedFilter;

            StandingsTitle.Text = $"Classificação ({standingCompetition})";
            StandingsGrid.ItemsSource = BuildStandings(_games, standingCompetition);
        }

        private void ApplyGamesFilter()
        {
            var games = _games;
            var selectedFilter = CompetitionFilterBox.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(selectedFilter) && selectedFilter != "Todas")
            {
                games = _games.Where(g => g.Competition == selectedFilter).ToList();
            }

            GamesList.Items.Clear();
            foreach (var g in games)
            {
                GamesList.Items.Add($"[{g.Competition}] {g.Date:yyyy-MM-dd} vs {g.Opponent} - {g.Result}");
            }

            var parsed = games.Where(g => g.TryGetGoals(out _, out _)).ToList();
            int wins = 0;
            int draws = 0;
            int losses = 0;
            foreach (var game in parsed)
            {
                game.TryGetGoals(out int gf, out int ga);
                if (gf > ga) wins++;
                else if (gf == ga) draws++;
                else losses++;
            }
            SummaryText.Text = $"Jogos: {games.Count} | V:{wins} E:{draws} D:{losses}";
            ApplyStandings();
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
            if (string.IsNullOrWhiteSpace(OpponentBox.Text))
            {
                MessageBox.Show("Indica o adversário.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = ResultBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(result) || !result.Contains('-'))
            {
                MessageBox.Show("Resultado inválido. Usa formato X-Y.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var game = new Game
            {
                Date = DatePicker.SelectedDate ?? DateTime.Now,
                Opponent = OpponentBox.Text.Trim(),
                Competition = CompetitionBox.SelectedValue?.ToString() ?? "Liga",
                Result = result
            };

            if (!game.TryGetGoals(out _, out _))
            {
                MessageBox.Show("Resultado inválido. Usa números inteiros no formato X-Y.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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

        private void CompetitionFilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyGamesFilter();
        }

        private static List<StandingRow> BuildStandings(List<Game> games, string competition)
        {
            var teamRows = new List<StandingRow>
            {
                new StandingRow { Team = "Minha Equipa" }
            };

            foreach (var game in games.Where(g => g.Competition == competition))
            {
                if (!game.TryGetGoals(out int gf, out int ga))
                {
                    continue;
                }

                var ourTeam = teamRows.First(r => r.Team == "Minha Equipa");
                var opponent = teamRows.FirstOrDefault(r => r.Team == game.Opponent);
                if (opponent == null)
                {
                    opponent = new StandingRow { Team = game.Opponent };
                    teamRows.Add(opponent);
                }

                ApplyGame(ourTeam, gf, ga);
                ApplyGame(opponent, ga, gf);
            }

            return teamRows
                .OrderByDescending(r => r.Points)
                .ThenByDescending(r => r.GoalDifference)
                .ThenByDescending(r => r.GoalsFor)
                .Select((row, index) =>
                {
                    row.Position = index + 1;
                    return row;
                })
                .ToList();
        }

        private static void ApplyGame(StandingRow row, int goalsFor, int goalsAgainst)
        {
            row.Played += 1;
            row.GoalsFor += goalsFor;
            row.GoalsAgainst += goalsAgainst;

            if (goalsFor > goalsAgainst)
            {
                row.Points += 3;
            }
            else if (goalsFor == goalsAgainst)
            {
                row.Points += 1;
            }
        }

        private class PerformanceRow
        {
            public int PlayerId { get; set; }
            public string PlayerName { get; set; } = string.Empty;
            public int Rating { get; set; }
        }

        private class StandingRow
        {
            public int Position { get; set; }
            public string Team { get; set; } = string.Empty;
            public int Played { get; set; }
            public int Points { get; set; }
            public int GoalsFor { get; set; }
            public int GoalsAgainst { get; set; }
            public int GoalDifference => GoalsFor - GoalsAgainst;
        }
    }
}
