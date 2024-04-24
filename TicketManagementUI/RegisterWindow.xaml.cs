using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        /// <summary>
        /// Конструктор класса-окна регистрации
        /// </summary>
        /// <param name="isCourier">Регистрируется курьер</param>
        public RegisterWindow(bool isCourier)
        {
            _usersManager = new UsersManager();
            _users = _usersManager.Read();
            _isCourier = isCourier;
            InitializeComponent();
        }
        /// <summary>
        /// Кнопка регистрации нового пользователя
        /// </summary>
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
        /// <summary>
        /// Обработчик события изменения текста в окне имения пользователя(для исключения ввода пробела)
        /// </summary>
        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserName.Text = UserName.Text.Replace(" ",string.Empty);
            UserName.SelectionStart = UserName.Text.Length;
        }
    }
}
