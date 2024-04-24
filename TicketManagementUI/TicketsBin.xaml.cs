using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для TicketsBin.xaml
    /// </summary>
    public partial class TicketsBin : Window
    {
        List<Ticket> _tickets;
        /// <summary>
        /// Конструктор класса-окна корзины
        /// </summary>
        /// <param name="tickets">Список выбранных мест</param>
        public TicketsBin(List<Ticket> tickets)
        {
            _tickets = tickets;
            InitializeComponent();
            TicketsDataDisplay.ItemsSource = _tickets.OrderBy(t => t.Type).ThenBy(t => t.Place);
            PriceBox.Content += _tickets.Sum(t => t.Cost).ToString();
        }
        /// <summary>
        /// Кнопка покупки
        /// </summary>
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Ticket ticket in _tickets)
                {
                    new TicketsManager().Create(ticket);
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
