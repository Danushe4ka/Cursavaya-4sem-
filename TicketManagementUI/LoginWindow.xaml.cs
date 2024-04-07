using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly UsersManager _usersManager;
        List<BaseUser> _users;
        public LoginWindow()
        {
            _usersManager = new UsersManager();
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _users = _usersManager.Read();
            try
            {
                Exception exception = new Exception("Неверное имя пользователя или пароль!");
                bool userExist = false;
                if (UserPassword.Password == string.Empty || UserName.Text == string.Empty)
                    throw exception;
                foreach (var user in _users)
                    if (user.UserName == UserName.Text && user.Password == UserPassword.Password)
                    {
                        MainWindow main = new MainWindow(user);
                        userExist = true;
                        main.Show();
                        this.Close();
                    }
                if (!userExist)
                    throw exception;                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _users = _usersManager.Read();
            RegisterWindow registerWindow = new RegisterWindow(false);
            registerWindow.ShowDialog();
        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserName.Text = UserName.Text.Replace(" ", string.Empty);
            UserName.SelectionStart = UserName.Text.Length;
        }
    }
}
