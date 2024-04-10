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
        readonly TicketTypesManager _ticketTypesManager = new TicketTypesManager();
        readonly TicketsManager _ticketsManager = new TicketsManager();
        readonly UsersManager _usersManager = new UsersManager();
        readonly ReportBuilder _reportBuilder = new ReportBuilder();
        List<Spectacle> _spectacles;
        List<TicketType> _ticketTypes;
        List<Ticket> _tickets;
        List<BaseUser> _users;
        public MainWindow(BaseUser user)
        {
            try
            {
                _user = user;
                InitializeComponent();
                CreateMainInterface();
                CreateUserInterface();
                TicketBuyButton.Visibility = Visibility.Visible;
                switch (user.RoleId)
                {
                    case (1):
                        CreateCourierInterface();
                        CreateAdminInterface();
                        break; 
                    case (2):
                        break;
                    case (3):
                        CreateCourierInterface();
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
            _ticketTypes = _ticketTypesManager.Read();
            _spectacles = _spectacleManager.Read();
            _tickets = _ticketsManager.Read();
            _users = _usersManager.Read();
            MainDataDisplay.ItemsSource = _spectacles.OrderBy(s => s.SpectacleDate);
            MainDataDisplay.SelectedIndex = 0;
            DateChoosementBox.ItemsSource = _spectacles.Select(d => d.SpectacleDate.Date).Distinct().ToList();
            GenreChoosementBox.ItemsSource = _spectacles.Select(g => g.SpectacleGenre).Distinct().ToList();
            CategoryChoosementBox.ItemsSource = _ticketTypes.Select(t => t.TypeName).Distinct().ToList();
        }
        void CreateUserInterface()
        {
            UserTicketsInfo.Visibility = Visibility.Visible;
            UserTicketsInfo.IsEnabled = true;
            updateUserInterface();
        }
        void updateUserInterface()
        {
            _tickets = _ticketsManager.Read();
            TicketsDataDisplay.ItemsSource = _tickets.Where(t => t.UserId == _user.UserId).OrderBy(t => t.SpectacleName).ThenBy(t => t.TypeName).ThenBy(t => t.Place);
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
            OrdersDataDisplay.ItemsSource = _tickets.Where(t => t.IsConfirmed == false).OrderBy(t => t.UserName).ThenBy(t => t.SpectacleName).ThenBy(t => t.TypeName).ThenBy(t => t.Place);
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
            UsersDataDisplay.ItemsSource = _users.Where(p => p.RoleId != 1).OrderBy(p => p.Role);
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
            MainDataDisplay.ItemsSource = _spectacles.Where(s => s.SpectacleDate.Date == Convert.ToDateTime(DateChoosementBox.SelectionBoxItem).Date);
            MainDataDisplay.SelectedIndex = 0;
        }

        private void ChooseGenreButton_Click(object sender, RoutedEventArgs e)
        {
            MainDataDisplay.ItemsSource = _spectacles.Where(s => s.SpectacleGenre == GenreChoosementBox.SelectionBoxItem.ToString());
            MainDataDisplay.SelectedIndex = 0;
        }
        private void ChooseCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            List<Spectacle> enabledItems = _spectacles.ToList();
            TicketType ticketType = null;
            foreach (TicketType type in _ticketTypes)
                if (type.TypeName == CategoryChoosementBox.SelectionBoxItem.ToString())
                    ticketType = type;
            int amountOfPlaces = ticketType.TypeAmount;
            foreach (Spectacle spectacle in _spectacles.ToList())
            {
                int blockedPlaces = 0;
                for (int i = 1; i <= amountOfPlaces; i++)
                {
                    foreach (Ticket ticket in _tickets.Where(t => t.TypeId == ticketType.TypeId && t.SpectacleID == spectacle.SpectacleId && t.Place == i))
                        blockedPlaces++;
                }
                if(blockedPlaces == amountOfPlaces)
                    enabledItems.Remove(spectacle);
            }
            MainDataDisplay.ItemsSource = enabledItems;
            MainDataDisplay.SelectedIndex = 0;
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
             BuyTicket buyTicket = new BuyTicket(_user.UserId, ((Spectacle)MainDataDisplay.SelectedItem).SpectacleId);
             buyTicket.ShowDialog();
             updateUserInterface();
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
            updateUserInterface();
            MessageBox.Show("Заказ(ы) успешно отменён(ы)!");
        }
        private void ConfrimOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket;
            foreach(var item  in OrdersDataDisplay.SelectedItems)
            {
                ticket = (Ticket)item;
                ticket.IsConfirmed = true;
                _ticketsManager.Update(ticket);
            }
            UpdateCourierInterface();
            updateUserInterface();
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
            updateUserInterface();
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
            _ticketTypes = _ticketTypesManager.Read();
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
            ReportsDisplay.Text = _reportBuilder.GetSalseReportBySpectacle();
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
