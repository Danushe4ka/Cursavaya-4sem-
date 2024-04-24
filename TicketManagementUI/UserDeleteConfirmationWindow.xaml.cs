using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using System.Windows;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для UserDeleteConfirmationWindow.xaml
    /// </summary>
    public partial class UserDeleteConfirmationWindow : Window
    {
        readonly BaseUser _user;
        readonly UsersManager _usersManager = new UsersManager();
        /// <summary>
        /// Конструктор класса-окна удаления пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public UserDeleteConfirmationWindow(BaseUser user)
        {
            _user = user;
            InitializeComponent();
        }
        /// <summary>
        /// Кнопка отмены удаления
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Кнопка подтверждения удаления
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                _usersManager.Delete(_user);
                MessageBox.Show("Пользователь был успешно удалён из системы!");
                this.Close();
            }
            catch
            { MessageBox.Show("Ошибка удаления пользователя!"); }
        }
    }
}
