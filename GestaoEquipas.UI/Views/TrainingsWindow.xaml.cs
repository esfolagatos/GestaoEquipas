using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System;
using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class TrainingsWindow : Window
    {
        private readonly TrainingService _service = new TrainingService();

        public TrainingsWindow()
        {
            InitializeComponent();
            LoadSessions();
        }

        private void LoadSessions()
        {
            SessionsList.Items.Clear();
            foreach (var s in _service.GetSessions())
            {
                SessionsList.Items.Add($"{s.Date:yyyy-MM-dd} - {s.Notes}");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var session = new TrainingSession
            {
                Date = DatePicker.SelectedDate ?? DateTime.Now,
                Notes = NotesBox.Text
            };
            _service.AddSession(session);
            LoadSessions();
            NotesBox.Text = "";
        }
    }
}
