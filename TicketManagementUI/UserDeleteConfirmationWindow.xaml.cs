using DBObjectsClassLibrary;
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
    /// Логика взаимодействия для UserDeleteConfirmationWindow.xaml
    /// </summary>
    public partial class UserDeleteConfirmationWindow : Window
    {
        readonly BaseUser _user;
        readonly UsersManager _usersManager = new UsersManager();
        public UserDeleteConfirmationWindow(BaseUser user)
        {
            _user = user;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
