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
    /// Логика взаимодействия для TicketCategoryPrice.xaml
    /// </summary>
    public partial class TicketCategoryPrice : Window
    {
        readonly TicketTypesManager ticketTypesManager = new TicketTypesManager();
        List<TicketType> ticketTypes;
        public TicketCategoryPrice()
        {
            InitializeComponent();
            updateTicketTypes();
        }
        void updateTicketTypes()
        {
            ticketTypes = ticketTypesManager.Read();
            TicketTypesData.ItemsSource = ticketTypes;
            TicketTypesData.SelectedIndex = 0;
        }

        private void TicketTypesData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TicketType type = (TicketType)TicketTypesData.SelectedItem;
            if(type != null)
                ChangedPriceBox.Text = type.TypeCost.ToString();
        }

        private void RedactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TicketType type = (TicketType)TicketTypesData.SelectedItem;
                ticketTypesManager.Update(new TicketType(type.TypeId, type.TypeName, type.TypeAmount, Convert.ToDouble(ChangedPriceBox.Text)));
                updateTicketTypes();
            }
            catch {MessageBox.Show("Неверный ввод цены на тип билета"); }
        }
    }
}
