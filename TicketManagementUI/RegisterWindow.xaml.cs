using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary;
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
using DBObjectsClassLibrary.Models;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        readonly UsersManager _usersManager;
        List<BaseUser> _users;
        bool _isCourier;
        public RegisterWindow(bool isCourier)
        {
            _usersManager = new UsersManager();
            _users = _usersManager.Read();
            _isCourier = isCourier;
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserPassword.Password == string.Empty || UserPassword.Password == string.Empty || UserName.Text == string.Empty)
                    throw new Exception("Все поля должны быть заполнены!");
                if (UserPassword.Password != UserPasswordConfirm.Password)
                    throw new Exception("Пароли не совпадают!");
                switch(_isCourier)
                {
                    case true:
                       _usersManager.Create(new Courier(UserName.Text, UserPassword.Password));
                       break;
                    case false:
                        _usersManager.Create(new RegUser(UserName.Text, UserPassword.Password));
                        break;
                }
                this.Close();
                MessageBox.Show("Пользователь успешно зарегистрирован");
            }
            catch (Exception ex)
            {
                if(ex is Npgsql.PostgresException)
                    MessageBox.Show("Имя пользователя занято!");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserName.Text = UserName.Text.Replace(" ",string.Empty);
            UserName.SelectionStart = UserName.Text.Length;
        }
    }
}
