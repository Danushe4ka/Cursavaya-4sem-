using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Utility;
using System;
using System.Linq;
using System.Windows;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для TicketsBin.xaml
    /// </summary>
    public partial class TicketsBin : Window
    {
        /// <summary>
        /// Конструктор класса-окна корзины
        /// </summary>
        public TicketsBin()
        {
            InitializeComponent();
            UpdateTickets();
        }
        /// <summary>
        /// Кнопка покупки билетов
        /// </summary>
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = 0;
                foreach (var ticket in Bin.GetInstance.GetBin)
                {
                    new TicketsManager().Create(ticket);
                    count++;
                }
                Bin.GetInstance.ClearBin();
                this.Close();
                MessageBox.Show($"Куплено {count} билетов");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Bin.GetInstance.ClearBin();
                this.Close();
            }
        }
        /// <summary>
        /// Кнопка удаления выбранных билетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(Ticket ticket in TicketsDataDisplay.SelectedItems)
                Bin.GetInstance.RemoveFromBin(ticket);
            UpdateTickets();
        }
        /// <summary>
        /// Метод обновления информации о билетах и общей стоимости 
        /// </summary>
        private void UpdateTickets()
        {
            TicketsDataDisplay.ItemsSource = Bin.GetInstance.GetBin.OrderBy(t => t.Type).ThenBy(t => t.Place);
            PriceBox.Content = "Общая стоимость: " + Bin.GetInstance.GetBin.Sum(t => t.Cost).ToString();
        }
    }
}
