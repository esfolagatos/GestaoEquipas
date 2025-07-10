using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Collections.Generic;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class TrainingsWindow : Window
    {
        private readonly TrainingService _service = new TrainingService();
        private readonly PlayerService _playerService = new PlayerService();
        private readonly AttendanceService _attendanceService = new AttendanceService();
        private readonly ExerciseService _exerciseService = new ExerciseService();

        private List<AttendanceRow> _rows = new List<AttendanceRow>();

        public TrainingsWindow()
        {
            InitializeComponent();
            LoadSessions();
            LoadPlayers();
            LoadExercises();
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

        private void LoadExercises()
        {
            ExercisesList.Items.Clear();
            foreach (var ex in _exerciseService.GetExercises())
            {
                ExercisesList.Items.Add(new System.Windows.Controls.ListBoxItem
                {
                    Content = ex.Name,
                    Tag = ex
                });
            }
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
            LoadExercises();
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            var ex = new Exercise
            {
                Name = ExerciseNameBox.Text,
                Description = ExerciseDescBox.Text
            };
            _exerciseService.AddExercise(ex);
            ExerciseNameBox.Text = "";
            ExerciseDescBox.Text = "";
            LoadExercises();
        }

        private void ExportPdf_Click(object sender, RoutedEventArgs e)
        {
            var sheet = new TrainingSheet
            {
                Date = DatePicker.SelectedDate ?? DateTime.Now,
                Notes = NotesBox.Text
            };
            foreach (System.Windows.Controls.ListBoxItem item in ExercisesList.SelectedItems)
            {
                if (item.Tag is Exercise ex)
                {
                    sheet.Exercises.Add(ex);
                }
            }

            var dlg = new Microsoft.Win32.SaveFileDialog { Filter = "PDF|*.pdf" };
            if (dlg.ShowDialog() == true)
            {
                var doc = new PdfDocument();
                var page = doc.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var fontTitle = new XFont("Verdana", 20, XFontStyle.Bold);
                var font = new XFont("Verdana", 12);

                int y = 40;
                gfx.DrawString($"Treino: {sheet.Date:yyyy-MM-dd}", fontTitle, XBrushes.Black, new XPoint(40, y));
                y += 30;
                gfx.DrawString(sheet.Notes, font, XBrushes.Black, new XPoint(40, y));
                y += 30;
                foreach (var ex in sheet.Exercises)
                {
                    gfx.DrawString($"- {ex.Name}: {ex.Description}", font, XBrushes.Black, new XPoint(50, y));
                    y += 20;
                }

                doc.Save(dlg.FileName);
            }
        }

        private class AttendanceRow
        {
            public int PlayerId { get; set; }
            public string PlayerName { get; set; } = string.Empty;
            public bool Present { get; set; }
        }
    }
}
