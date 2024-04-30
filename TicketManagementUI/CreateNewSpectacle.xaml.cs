using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Spectacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для CreateNewSpectacle.xaml
    /// </summary>
    public partial class CreateNewSpectacle : Window
    {
        readonly SpectaclesManager _spectaclesManager = new SpectaclesManager();
        /// <summary>
        /// Конструктор класса-окна для добавления спектакля
        /// </summary>
        public CreateNewSpectacle()
        {
            List<Spectacle> spectacles = _spectaclesManager.Read();
            InitializeComponent();
            SpectacleGenreBox.ItemsSource = spectacles.Select(g => g.Genre).Distinct().ToList();
            SpectacleGenreBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Кнопка добавления нового спектакля
        /// </summary>
        private void CreateSpectacleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SpectacleNameBox.Text != string.Empty && SpectacleAuthorBox.Text != string.Empty && SpectacleDatePicker.SelectedDate != null)
                {
                    DateTime dateCal = Convert.ToDateTime(SpectacleDatePicker.SelectedDate);
                    DateTime date = new DateTime(dateCal.Year, dateCal.Month, dateCal.Day);
                    switch (SpectacleGenreBox.SelectedItem.ToString())
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
            catch(Exception ex) {  MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// Обработчик события при вводе текста в окна для ввода часов и минут для допуска ввода только чисел
        /// </summary>
        private void TimeBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
