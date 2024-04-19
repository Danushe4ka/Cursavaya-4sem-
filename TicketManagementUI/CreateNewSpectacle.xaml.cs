using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Spectacles;
using DBObjectsClassLibrary.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для CreateNewSpectacle.xaml
    /// </summary>
    public partial class CreateNewSpectacle : Window
    {
        readonly SpectaclesManager _spectaclesManager = new SpectaclesManager();
        public CreateNewSpectacle()
        {
            List<Spectacle> spectacles = _spectaclesManager.Read();
            InitializeComponent();
            SpectacleGenreBox.ItemsSource = spectacles.Select(g => g.Genre).Distinct().ToList();
            SpectacleGenreBox.SelectedIndex = 0;
        }

        private void CreateSpectacleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpectacleNameBox.Text != string.Empty && SpectacleAuthorBox.Text != string.Empty && SpectacleDatePicker.SelectedDate != null)
            {
                DateTime date = Convert.ToDateTime(SpectacleDatePicker.SelectedDate);
                switch(SpectacleGenreBox.SelectedItem.ToString())
                {
                    case "Трагедия":
                        _spectaclesManager.Create(new TragedySpectacle(SpectacleNameBox.Text, SpectacleAuthorBox.Text, date));
                        break;
                    case "Комедия":
                        _spectaclesManager.Create(new ComedySpectacle(SpectacleNameBox.Text, SpectacleAuthorBox.Text, date));
                        break;
                    case "Драма":
                        _spectaclesManager.Create(new DramaSpectacle(SpectacleNameBox.Text, SpectacleAuthorBox.Text, date));
                        break;
                    case "Роман":
                        _spectaclesManager.Create(new NovelSpectacle(SpectacleNameBox.Text, SpectacleAuthorBox.Text, date));
                        break;
                    default:
                        throw new Exception("Неопознанный жанр спектакля");
                }
                MessageBox.Show("Спектакль успешно добавлен");
                this.Close();
            }
            else
                MessageBox.Show("Все поля должны быть заполнены!");
        }
    }
}
