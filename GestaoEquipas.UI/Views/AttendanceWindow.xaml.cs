using GestaoEquipas.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GestaoEquipas.UI.Views
{
    public partial class AttendanceWindow : Window
    {
        private readonly TrainingService _trainingService = new TrainingService();
        private readonly PlayerService _playerService = new PlayerService();
        private readonly AttendanceService _attendanceService = new AttendanceService();

        private readonly List<SessionOption> _sessions = new List<SessionOption>();

        public AttendanceWindow()
        {
            InitializeComponent();
            LoadSessions();
            LoadSummary();
        }

        private void LoadSessions()
        {
            _sessions.Clear();
            foreach (var session in _trainingService.GetSessions().OrderByDescending(s => s.Date))
            {
                _sessions.Add(new SessionOption
                {
                    Id = session.Id,
                    Description = $"{session.Date:yyyy-MM-dd} - {session.Notes}"
                });
            }

            SessionCombo.ItemsSource = _sessions;
            SessionCombo.DisplayMemberPath = nameof(SessionOption.Description);
            SessionCombo.SelectedValuePath = nameof(SessionOption.Id);

            if (_sessions.Count > 0)
            {
                SessionCombo.SelectedIndex = 0;
            }
            else
            {
                AttendanceGrid.ItemsSource = null;
            }
        }

        private void SessionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSessionAttendance();
        }

        private void LoadSessionAttendance()
        {
            if (SessionCombo.SelectedItem is not SessionOption selected)
            {
                AttendanceGrid.ItemsSource = null;
                return;
            }

            var players = _playerService.GetPlayers().ToDictionary(p => p.Id, p => p.Name);
            var records = _attendanceService.GetBySession(selected.Id).ToList();

            var rows = records
                .Select(r => new AttendanceRow
                {
                    PlayerName = players.TryGetValue(r.PlayerId, out var name) ? name : $"Atleta #{r.PlayerId}",
                    PresenceText = r.Present ? "Presente" : "Ausente"
                })
                .OrderBy(r => r.PlayerName)
                .ToList();

            AttendanceGrid.ItemsSource = rows;
        }

        private void LoadSummary()
        {
            var players = _playerService.GetPlayers().ToList();
            var sessions = _trainingService.GetSessions().ToList();
            var rows = new List<SummaryRow>();

            foreach (var player in players)
            {
                int totalSessions = 0;
                int presents = 0;

                foreach (var session in sessions)
                {
                    var record = _attendanceService
                        .GetBySession(session.Id)
                        .FirstOrDefault(r => r.PlayerId == player.Id);

                    if (record == null)
                    {
                        continue;
                    }

                    totalSessions += 1;
                    if (record.Present)
                    {
                        presents += 1;
                    }
                }

                double percentageValue = totalSessions == 0
                    ? 0
                    : Math.Round((double)presents / totalSessions * 100, 1);

                rows.Add(new SummaryRow
                {
                    PlayerName = player.Name,
                    Sessions = totalSessions,
                    Presents = presents,
                    Percentage = $"{percentageValue}%",
                    PercentageValue = percentageValue
                });
            }

            SummaryGrid.ItemsSource = rows.OrderByDescending(r => r.PercentageValue).ToList();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadSessions();
            LoadSessionAttendance();
            LoadSummary();
        }

        private class SessionOption
        {
            public int Id { get; set; }
            public string Description { get; set; } = string.Empty;
        }

        private class AttendanceRow
        {
            public string PlayerName { get; set; } = string.Empty;
            public string PresenceText { get; set; } = string.Empty;
        }

        private class SummaryRow
        {
            public string PlayerName { get; set; } = string.Empty;
            public int Sessions { get; set; }
            public int Presents { get; set; }
            public string Percentage { get; set; } = string.Empty;
            public double PercentageValue { get; set; }
        }
    }
}
