using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class TrainingsWindow : Window
    {
        private readonly TrainingService _service = new TrainingService();
        private readonly PlayerService _playerService = new PlayerService();
        private readonly AttendanceService _attendanceService = new AttendanceService();

        private List<AttendanceRow> _rows = new List<AttendanceRow>();

        public TrainingsWindow()
        {
            InitializeComponent();
            LoadSessions();
            LoadPlayers();
        }

        private void LoadSessions()
        {
            SessionsList.Items.Clear();
            foreach (var s in _service.GetSessions())
            {
                SessionsList.Items.Add($"{s.Date:yyyy-MM-dd} - {s.Notes}");
            }
        }

        private void LoadPlayers()
        {
            _rows = new List<AttendanceRow>();
            foreach (var p in _playerService.GetPlayers())
            {
                _rows.Add(new AttendanceRow { PlayerId = p.Id, PlayerName = p.Name });
            }
            AttendanceGrid.ItemsSource = _rows;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var session = new TrainingSession
            {
                Date = DatePicker.SelectedDate ?? DateTime.Now,
                Notes = NotesBox.Text
            };
            int sessionId = _service.AddSession(session);

            var records = new List<AttendanceRecord>();
            foreach (var row in _rows)
            {
                records.Add(new AttendanceRecord
                {
                    TrainingSessionId = sessionId,
                    PlayerId = row.PlayerId,
                    Present = row.Present
                });
            }
            _attendanceService.AddRecords(records);

            LoadSessions();
            NotesBox.Text = "";
            LoadPlayers();
        }

        private class AttendanceRow
        {
            public int PlayerId { get; set; }
            public string PlayerName { get; set; } = string.Empty;
            public bool Present { get; set; }
        }
    }
}
