using DBObjectsClassLibrary.DataAccess;
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
    /// Логика взаимодействия для TicketCategoryPrice.xaml
    /// </summary>
    public partial class TicketCategoryPrice : Window
    {
        readonly TicketTypesManager _ticketTypesManager = new TicketTypesManager();
        public TicketCategoryPrice()
        {
            InitializeComponent();
            TypeChoosementBox.ItemsSource = _ticketTypesManager.GetTypeNames();
            TypeChoosementBox.SelectedIndex = 0;
        }

        private void TypeChoosementBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PriceBox.Text = _ticketTypesManager.GetTicketTypePrice(TypeChoosementBox.SelectedItem.ToString()).ToString();
        }

        private void ConfirmChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double price = Convert.ToDouble(PriceBox.Text);
                _ticketTypesManager.ChangeTicketTypePrice(TypeChoosementBox.SelectedItem.ToString(), price);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неверный ввод данныйх\nОбратите внимание!\nДанные представлены в числовом виде");
            }
        }

        private void PriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
