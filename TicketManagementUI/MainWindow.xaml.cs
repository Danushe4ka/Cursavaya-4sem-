using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Utility;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _changeUser;
        readonly BaseUser _user;
        readonly SpectaclesManager _spectacleManager = new SpectaclesManager();
        readonly TicketsManager _ticketsManager = new TicketsManager();
        readonly UsersManager _usersManager = new UsersManager();
        readonly ReportBuilder _reportBuilder = new ReportBuilder();
        List<Spectacle> _spectacles;
        List<Ticket> _tickets;
        List<BaseUser> _users;
        List <string> _ticketTypes;
        public MainWindow(BaseUser user)
        {
            try
            {
                _user = user;
                InitializeComponent();
                CreateMainInterface();
                TicketBuyButton.Visibility = Visibility.Visible;
                switch (user.Role)
                {
                    case ("Admin"):
                        CreateUserInterface();
                        CreateCourierInterface();
                        CreateAdminInterface();
                        break; 
                    case ("Courier"):
                        CreateUserInterface();
                        CreateCourierInterface();
                        break;
                    case ("RegUser"):
                        CreateUserInterface();
                        break;
                    default:
                        throw new Exception("Ошибка типа пользователя, используется недействительная модель данных!");
                }
            }
            catch (Exception ex) {MessageBox.Show(ex.Message); }
        }
        public MainWindow()
        {
            InitializeComponent();
            CreateMainInterface();
        }
        void CreateMainInterface()
        {
            _changeUser = false;
            _spectacles = _spectacleManager.Read();
            _tickets = _ticketsManager.Read();
            _users = _usersManager.Read();
            _ticketTypes = new TicketTypesManager().GetTypeNames();
            MainDataDisplay.ItemsSource = _spectacles.OrderBy(s => s.SpectacleDate);
            MainDataDisplay.SelectedIndex = 0;
            DateChoosementBox.ItemsSource = _spectacles.Select(d => d.SpectacleDate.Date).Distinct().ToList();
            GenreChoosementBox.ItemsSource = _spectacles.Select(g => g.Genre).Distinct().ToList();
            CategoryChoosementBox.ItemsSource = _ticketTypes;
        }
        void CreateUserInterface()
        {
            UserTicketsInfo.Visibility = Visibility.Visible;
            UserTicketsInfo.IsEnabled = true;
            UpdateUserInterface();
        }
        void UpdateUserInterface()
        {
            _tickets = _ticketsManager.Read();
            TicketsDataDisplay.ItemsSource = _tickets.Where(t => t.User.UserName == _user.UserName).OrderBy(t => t.IsConfirmed).ThenBy(t => t.Spectacle.SpectacleName);
            TicketsDataDisplay.SelectedIndex = 0;
            if(TicketsDataDisplay.SelectedItem == null)
                ReturnTicketButton.IsEnabled = false;
            else
                ReturnTicketButton.IsEnabled = true;
        }
        void CreateCourierInterface()
        {
            CourierInfo.Visibility = Visibility.Visible;
            CourierInfo.IsEnabled = true;
            UpdateCourierInterface();
        }
        void UpdateCourierInterface()
        {
            _tickets = _ticketsManager.Read();
            OrdersDataDisplay.ItemsSource = _tickets.Where(t => t.IsConfirmed == false).OrderBy(t => t.User.UserName).ThenBy(t => t.Spectacle.SpectacleName);
            OrdersDataDisplay.SelectedIndex = 0;
            if (OrdersDataDisplay.SelectedItem == null)
                ConfrimOrderButton.IsEnabled = false;
            else
                ConfrimOrderButton.IsEnabled = true;
        }
        void CreateAdminInterface()
        {
            UsersInfo.Visibility = Visibility.Visible;
            UsersInfo.IsEnabled = true;
            SpectaclesInfo.Visibility = Visibility.Visible;
            SpectaclesInfo.IsEnabled = true;
            Reports.Visibility = Visibility.Visible;
            Reports.IsEnabled = true;
            UpdateUsersPanel();
            UpdateSpectaclesPanel();
        }
        void UpdateUsersPanel()
        {
            _users = _usersManager.Read();
            UsersDataDisplay.ItemsSource = _users.Where(p => p.Role != "Admin").OrderBy(p => p.Role);
            UsersDataDisplay.SelectedIndex = 0;
            if (UsersDataDisplay.SelectedItem == null)
                DeleteUserButton.IsEnabled = false;
            else
                DeleteUserButton.IsEnabled = true;
        }
        void UpdateSpectaclesPanel()
        {
            _spectacles = _spectacleManager.Read();
            SpectaclesDataDisplay.ItemsSource = _spectacles.OrderBy(s => s.SpectacleDate);
            SpectaclesDataDisplay.SelectedIndex = 0;
            if(SpectaclesDataDisplay.SelectedItem == null)
                DeleteSpectacleButton.IsEnabled = false;
            else
                DeleteSpectacleButton.IsEnabled = true;
        }
        private void ChangeUserButton_Click(object sender, RoutedEventArgs e)
        {
            _changeUser = true;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            this.Close();
        }

        private void ChooseAll_Click(object sender, RoutedEventArgs e)
        {
            MainDataDisplay.ItemsSource = _spectacles.OrderBy(s => s.SpectacleDate);
            DateChoosementBox.SelectedIndex = -1;
            GenreChoosementBox.SelectedIndex = -1;
            CategoryChoosementBox.SelectedIndex = -1;
            ChooseDateButton.IsEnabled = false;
            ChooseGenreButton.IsEnabled = false;
            ChooseCategoryButton.IsEnabled = false;
        }
        private void ChooseDateButton_Click(object sender, RoutedEventArgs e)
        {
            MainDataDisplay.ItemsSource = _spectacles.Where(s => s.SpectacleDate.Date == Convert.ToDateTime(DateChoosementBox.SelectionBoxItem).Date).OrderBy(s => s.SpectacleDate);
            MainDataDisplay.SelectedIndex = 0;
        }

        private void ChooseGenreButton_Click(object sender, RoutedEventArgs e)
        {
            MainDataDisplay.ItemsSource = _spectacles.Where(s => s.Genre == GenreChoosementBox.SelectionBoxItem.ToString()).OrderBy(s => s.SpectacleDate);
            MainDataDisplay.SelectedIndex = 0;
        }
        private void ChooseCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            List<Spectacle> enabledItems = _spectacles;
            string ticketType = CategoryChoosementBox.SelectedValue.ToString();
            int amountOfPlaces = new TicketTypesManager().GetTicketTypePlaceAmount(ticketType);
            foreach (Spectacle spectacle in _spectacles.ToList())
            {
                int blockedPlaces = 0;
                for (int i = 1; i <= amountOfPlaces; i++)
                {
                    foreach (Ticket ticket in _tickets.Where(t => t.Type == ticketType && t.Spectacle.SpectacleDate == spectacle.SpectacleDate && t.Place == i))
                        blockedPlaces++;
                }
                if(blockedPlaces == amountOfPlaces)
                    enabledItems.Remove(spectacle);
            }
            MainDataDisplay.ItemsSource = enabledItems.OrderBy(s => s.SpectacleDate);
            MainDataDisplay.SelectedIndex = 0;
            _spectacles = _spectacleManager.Read();
        }

        private void GenreChoosementBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChooseGenreButton.IsEnabled = true;
        }
        private void DateChoosementBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChooseDateButton.IsEnabled = true;
        }

        private void CategoryChoosementBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChooseCategoryButton.IsEnabled = true;
        }

        private void TicketBuyButton_Click(object sender, RoutedEventArgs e)
        {
             BuyTicket buyTicket = new BuyTicket(_user, ((Spectacle)MainDataDisplay.SelectedItem));
             buyTicket.ShowDialog();
             UpdateUserInterface();
             UpdateCourierInterface();
        }
        private void ReturnTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket;
            foreach (var item in TicketsDataDisplay.SelectedItems)
            {
                ticket = (Ticket)item;
                _ticketsManager.Delete(ticket);
            }
            UpdateUserInterface();
            UpdateCourierInterface();
            MessageBox.Show("Заказ(ы) успешно отменён(ы)!");
        }
        private void ConfrimOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket;
            foreach(var item in OrdersDataDisplay.SelectedItems)
            {
                ticket = (Ticket)item;
                ticket.IsConfirmed = true;
                _ticketsManager.Update(ticket);
            }
            UpdateCourierInterface();
            UpdateUserInterface();
            MessageBox.Show("Заказ(ы) оформлены!");
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserDeleteConfirmationWindow userDeleteConfirmationWindow = new UserDeleteConfirmationWindow((BaseUser)UsersDataDisplay.SelectedItem);
            userDeleteConfirmationWindow.ShowDialog();
            UpdateUsersPanel();
        }
        private void CreateCourierButton_Click(object sender, RoutedEventArgs e)
        {
            _users = _usersManager.Read();
            RegisterWindow registerWindow = new RegisterWindow(true);
            registerWindow.ShowDialog();
            UpdateUsersPanel();
        }
        private void DeleteSpectacleButton_Click(object sender, RoutedEventArgs e)
        {
            _spectacleManager.Delete((Spectacle)SpectaclesDataDisplay.SelectedItem);
            UpdateSpectaclesPanel();
            UpdateCourierInterface();
            UpdateUserInterface();
            CreateMainInterface();
            MessageBox.Show("Спектакль был успешно удалён из системы");
        }
        public void CreateSpectacleButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewSpectacle createNewSpectacle = new CreateNewSpectacle();
            createNewSpectacle.ShowDialog();
            UpdateSpectaclesPanel();
            CreateMainInterface();
        }
        public void ChangeTicketCategoryPriceButton_Click(object sender, RoutedEventArgs e)
        {
            TicketCategoryPrice ticketCategoryPrice = new TicketCategoryPrice();
            ticketCategoryPrice.ShowDialog();
            _tickets = _ticketsManager.Read();
            UpdateUserInterface();
        }
        public void CreateMainReport_Click(object sender, RoutedEventArgs e)
        {
            ReportsDisplay.Text = _reportBuilder.GetTotalSalesReport();
        }
        public void CreateTicketReport_Click(object sender, RoutedEventArgs e)
        {
            ReportsDisplay.Text = _reportBuilder.GetSalesReportByTicketType();
        }
        public void CreateSpectaclesReport_Click(object sender, RoutedEventArgs e)
        {
            ReportsDisplay.Text = _reportBuilder.GetSalesReportBySpectacle();
        }
        public void SaveReportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() != false)
            {
                string fileName = saveFileDialog.FileName;
                File.WriteAllText(fileName, ReportsDisplay.Text);
                MessageBox.Show("Отчёт успешно сохранён!");
            }
        }

        private void MainDataDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MainDataDisplay.SelectedItem == null)
                TicketBuyButton.IsEnabled = false;
            else
                TicketBuyButton.IsEnabled = true;
        }
        private void MainWindow_Closing(object sender, EventArgs e)
        {
            if(!_changeUser)
                DBConnectionManager.GetInstance.CloseConnection();
        }
    }
}
