using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly UsersManager _usersManager;
        List<BaseUser> _users;
        bool _otherOpened;
        /// <summary>
        /// Конструктор класса-окна входа
        /// </summary>
        public LoginWindow()
        {
            _usersManager = new UsersManager();
            _otherOpened = false;
            InitializeComponent();
        }

        /// <summary>
        /// Кнопка входа в систему
        /// </summary>
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
                        _otherOpened = true;
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
        /// <summary>
        /// Кнопка входа в систему как гость
        /// </summary>
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            _otherOpened = true;
            this.Close();
        }
        /// <summary>
        /// Кнопка перехода в окно регистрации
        /// </summary>
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _users = _usersManager.Read();
            RegisterWindow registerWindow = new RegisterWindow(false);
            registerWindow.ShowDialog();
        }
        /// <summary>
        /// Обработчик события изменения текста в поле ввода имения для исключения ввода символа ' '
        /// </summary>
        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserName.Text = UserName.Text.Replace(" ", string.Empty);
            UserName.SelectionStart = UserName.Text.Length;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!_otherOpened)
                DBConnectionManager.GetInstance.CloseConnection();
        }
    }
}
