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
    /// Логика взаимодействия для TicketsBin.xaml
    /// </summary>
    public partial class TicketsBin : Window
    {
        List<Ticket> _tickets;
        public TicketsBin(List<Ticket> tickets)
        {
            _tickets = tickets;
            InitializeComponent();
            TicketsDataDisplay.ItemsSource = _tickets.OrderBy(t => t.Type).ThenBy(t => t.Place);
            PriceBox.Content += _tickets.Sum(t => t.Cost).ToString();
        }

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
