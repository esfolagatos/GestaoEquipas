using GestaoEquipas.Business.Services;
using GestaoEquipas.Data.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GestaoEquipas.UI.Views
{
    public partial class CompetitionsWindow : Window
    {
        private readonly CompetitionService _service = new CompetitionService();

        public CompetitionsWindow()
        {
            InitializeComponent();
            LoadCompetitions();
        }

        private void LoadCompetitions()
        {
            CompetitionsGrid.ItemsSource = _service.GetCompetitions().ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text.Trim();
            var season = SeasonBox.Text.Trim();
            var type = (TypeBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Liga";

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(season))
            {
                MessageBox.Show("Preencha Nome e Época.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _service.AddCompetition(new Competition
            {
                Name = name,
                Type = type,
                Season = season
            });

            NameBox.Text = string.Empty;
            LoadCompetitions();
        }
    }
}
