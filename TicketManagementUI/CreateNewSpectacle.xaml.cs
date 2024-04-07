using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            InitializeComponent();
        }

        private void CreateSpectacleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpectacleNameBox.Text != string.Empty && SpectacleAuthorBox.Text != string.Empty && SpectacleGenreBox.Text != string.Empty && SpectacleDatePicker.SelectedDate != null)
            {
                _spectaclesManager.Create(new Spectacle(0, SpectacleNameBox.Text, SpectacleAuthorBox.Text, SpectacleGenreBox.Text, Convert.ToDateTime(SpectacleDatePicker.SelectedDate)));
                MessageBox.Show("Спектакль успешно добавлен");
                this.Close();
            }
            else
                MessageBox.Show("Все поля должны быть заполнены!");
        }
    }
}
