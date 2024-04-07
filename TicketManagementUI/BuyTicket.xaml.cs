using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Shapes;

namespace TicketManagementUI
{
    /// <summary>
    /// Логика взаимодействия для BuyTicket.xaml
    /// </summary>
    public partial class BuyTicket : Window
    {
        int _userId;
        int _spectacleId;
        readonly TicketTypesManager _ticketTypesManager = new TicketTypesManager();
        readonly TicketsManager _ticketManager = new TicketsManager();
        List<TicketType> _ticketTypes;
        List<Ticket> _tickets;
        TicketType _ticketType;
        public BuyTicket(int userId, int spectacleId)
        {
            _ticketTypes = _ticketTypesManager.Read();
            _tickets = _ticketManager.Read();
            _userId = userId;
            _spectacleId = spectacleId;
            InitializeComponent();
            TypeSelectionBox.ItemsSource = _ticketTypes.Select(t => t.TypeName).Distinct().ToList();
        }

        private void TypeSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(TicketType ticketType in _ticketTypes)
                if(ticketType.TypeName == TypeSelectionBox.SelectedItem.ToString())
                    _ticketType = ticketType;
            PlaceSelectionBox.ItemsSource = selectedTypeFreePlaces();
            PlaceSelectionBox.SelectedIndex = 0;
            if(selectedTypeFreePlaces().Count > 0)
                PlaceSelectionBox.IsEnabled = true;
            else
                PlaceSelectionBox.IsEnabled = false;
        }
        private List<int> selectedTypeFreePlaces()
        {
            int amountOfPlaces = _ticketType.TypeAmount;
            List<int> places = new List<int>();
            for (int i = 1; i <= amountOfPlaces; i++)
            {
                bool isReq = true;
                foreach (Ticket ticket in _tickets.Where(t => t.TypeId == _ticketType.TypeId && t.SpectacleID == _spectacleId && t.Place == i))
                    isReq = false;
                if(isReq)
                    places.Add(i);
            }
            return places;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int place = Convert.ToInt32(PlaceSelectionBox.SelectedItem);
                if (place > 0 && place <= _ticketType.TypeAmount)
                {
                    Ticket ticket = new Ticket(0, _userId, _spectacleId, _ticketType.TypeId, place);
                    _ticketManager.Create(ticket);
                    this.Close();
                }
                else throw new Exception("Выбранное место не существует!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PlaceSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(selectedTypeFreePlaces().Count > 0)
                BuyButton.IsEnabled = true;
            else
                BuyButton.IsEnabled = false;
        }
    }
}
