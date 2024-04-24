using DBObjectsClassLibrary.DataAccess;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для TicketCategoryPrice.xaml
    /// </summary>
    public partial class TicketCategoryPrice : Window
    {
        readonly TicketTypesManager _ticketTypesManager = new TicketTypesManager();
        /// <summary>
        /// Конструктор класса-окна изменения цены на тип
        /// </summary>
        public TicketCategoryPrice()
        {
            InitializeComponent();
            TypeChoosementBox.ItemsSource = _ticketTypesManager.GetTypeNames();
            TypeChoosementBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Обрабочтик события изменения типа билета и передачи его цены в окно изменения
        /// </summary>
        private void TypeChoosementBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PriceBox.Text = _ticketTypesManager.GetTicketTypePrice(TypeChoosementBox.SelectedItem.ToString()).ToString();
        }
        /// <summary>
        /// Кнопка подтверждения изменений
        /// </summary>
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
    }
}
