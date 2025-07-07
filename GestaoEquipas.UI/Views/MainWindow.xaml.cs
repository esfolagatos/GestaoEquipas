using System.Windows;

namespace GestaoEquipas.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Players_Click(object sender, RoutedEventArgs e)
        {
            var win = new PlayersWindow();
            win.ShowDialog();
        }

        private void Trainings_Click(object sender, RoutedEventArgs e)
        {
            var win = new TrainingsWindow();
            win.ShowDialog();
        }

        private void Games_Click(object sender, RoutedEventArgs e)
        {
            var win = new GamesWindow();
            win.ShowDialog();
        }

        private void Tactics_Click(object sender, RoutedEventArgs e)
        {
            var win = new TacticsWindow();
            win.ShowDialog();
        }
    }
}
