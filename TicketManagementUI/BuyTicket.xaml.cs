using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Sockets;
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
        BaseUser _user;
        Spectacle _spectacle;
        readonly TicketsManager _ticketManager = new TicketsManager();
        readonly TicketTypesManager _typesManager = new TicketTypesManager();
        List<Ticket> _tickets;
        public BuyTicket(BaseUser user, Spectacle spectacle)
        {
            _user = user;
            _spectacle = spectacle;
            _tickets = _ticketManager.Read();
            InitializeComponent();
            TypeSelectionBox.ItemsSource = _typesManager.GetTypeNames();
        }

        private void TypeSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaceSelectionBox.ItemsSource = SelectedTypeFreePlaces();
            PlaceSelectionBox.SelectedIndex = 0;
            if (SelectedTypeFreePlaces().Count > 0)
            {
                PlaceSelectionBox.IsEnabled = true;
                PriceBox.Text = _typesManager.GetTicketTypePrice(TypeSelectionBox.SelectedItem.ToString()).ToString();
            }
            else
            {
                PlaceSelectionBox.IsEnabled = false;
                PriceBox.Text = "----";
            }
        }
        private List<int> SelectedTypeFreePlaces()
        {
            string ticketType = TypeSelectionBox.SelectedItem.ToString();
            int amountOfPlaces = _typesManager.GetTicketTypePlaceAmount(ticketType);
            List<int> places = new List<int>();
            for (int i = 1; i <= amountOfPlaces; i++)
            {
                bool isReq = true;
                foreach (Ticket ticket in _tickets.Where(t => t.Type == ticketType && t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i))
                    isReq = false;
                if(isReq)
                    places.Add(i);
            }
            return places;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ticketType = TypeSelectionBox.SelectedItem.ToString();
            try
            {
                double cost = _typesManager.GetTicketTypePrice(ticketType);
                int amount = _typesManager.GetTicketTypePlaceAmount(ticketType);
                int place = Convert.ToInt32(PlaceSelectionBox.SelectedItem);
                if (place > 0 && place <= _typesManager.GetTicketTypePlaceAmount(ticketType))
                {
                    Ticket ticket;
                    switch (ticketType)
                    {
                        case "Партер":
                            ticket = new ParterTicket(_user,_spectacle,cost,place,amount);
                            _ticketManager.Create(ticket);
                            break;
                        case "Амфитеатр":
                            ticket = new AmphitheatreTicket(_user, _spectacle, cost, place, amount);
                            _ticketManager.Create(ticket);
                            break;
                        case "Бельэтаж":
                            ticket = new BeletageTicket(_user, _spectacle, cost, place, amount);
                            _ticketManager.Create(ticket);
                            break;
                        default:
                            throw new Exception("Неопознанный тип билета");
                    }
                    
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
            if(SelectedTypeFreePlaces().Count > 0)
                BuyButton.IsEnabled = true;
            else
                BuyButton.IsEnabled = false;
        }
    }
}
