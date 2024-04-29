using DBObjectsClassLibrary.DataAccess;
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
            TicketsDataDisplay.ItemsSource = Bin.GetInstance.GetBin.OrderBy(t => t.Type).ThenBy(t => t.Place);
            PriceBox.Content += Bin.GetInstance.GetBin.Sum(t => t.Cost).ToString();
        }
        /// <summary>
        /// Кнопка покупки
        /// </summary>
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var ticket in Bin.GetInstance.GetBin)
                {
                    new TicketsManager().Create(ticket);
                }
                Bin.GetInstance.ClearBin();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Bin.GetInstance.ClearBin();
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Bin.GetInstance.ClearBin();
        }
    }
}
