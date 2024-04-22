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
        int _lastSelectedPlacePt = 0;
        int _lastSelectedPlaceAm = 0;
        int _lastSelectedPlaceBl = 0;
        public BuyTicket(BaseUser user, Spectacle spectacle)
        {
            _user = user;
            _spectacle = spectacle;
            _tickets = _ticketManager.Read();
            InitializeComponent();
            TypeSelectionBox.ItemsSource = _typesManager.GetTypeNames();
            UpdateTheatrePlaces();
        }

        private void TypeSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedTypeFreePlaces().Count > 0)
            {
                PlaceSelectionBox.IsEnabled = true;
                PlaceSelectionBox.SelectedItem = SelectedTypeFreePlaces()[0];
                PlaceSelectionBox.ItemsSource = SelectedTypeFreePlaces();
            }
            else
            {
                PlaceSelectionBox.IsEnabled = false;
                PlaceSelectionBox.ItemsSource = SelectedTypeFreePlaces();
                UpdatePlaces();
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
        private void UpdateTheatrePlaces()
        {
            for(int i = 1; i <= 25; i++)
                foreach (Ticket ticket in _tickets.Where(t => t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i && t.Type == "Партер" && ParterPlaces.Children[i - 1] != null))
                    ParterPlaces.Children[i - 1].Opacity = 0.2;
            for (int i = 1; i <= 10; i++)
                foreach (Ticket ticket in _tickets.Where(t => t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i))
                {
                    if (AmphitheatrePlaces.Children[i - 1] != null && ticket.Type == "Амфитеатр")
                        AmphitheatrePlaces.Children[i - 1].Opacity = 0.2;
                    else if (BeletagePlaces.Children[i - 1] != null && ticket.Type == "Бельэтаж")
                        BeletagePlaces.Children[i - 1].Opacity = 0.2;
                }
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
            {
                BuyButton.IsEnabled = true;
                int selectedIndex = Convert.ToInt32(PlaceSelectionBox.SelectedItem) - 1;
                UpdatePlaces();
                switch (TypeSelectionBox.SelectedItem.ToString())
                {
                    case "Партер":
                        Label selectedLabel = (Label)ParterPlaces.Children[selectedIndex];
                        selectedLabel.Background = Brushes.Aquamarine;
                        ParterPlaces.Children[selectedIndex] = selectedLabel;
                        _lastSelectedPlacePt = selectedIndex;
                    break;
                    case "Амфитеатр":
                        
                        Label selectedLabelA = (Label)AmphitheatrePlaces.Children[selectedIndex];
                        selectedLabelA.Background = Brushes.CornflowerBlue;
                        AmphitheatrePlaces.Children[selectedIndex] = selectedLabelA;
                        _lastSelectedPlaceAm = selectedIndex;
                        break;
                    case "Бельэтаж":
                        
                        Label selectedLabelB = (Label)BeletagePlaces.Children[selectedIndex];
                        selectedLabelB.Background = Brushes.Blue;
                        BeletagePlaces.Children[selectedIndex] = selectedLabelB;
                        _lastSelectedPlaceBl = selectedIndex;
                        break;
                }
            }
            else
                BuyButton.IsEnabled = false;
        }
        private void UpdatePlaces()
        {
            Label lastSelectedLabel = (Label)ParterPlaces.Children[_lastSelectedPlacePt];
            lastSelectedLabel.Background = null;
            ParterPlaces.Children[_lastSelectedPlacePt] = lastSelectedLabel;

            Label lastSelectedLabelA = (Label)AmphitheatrePlaces.Children[_lastSelectedPlaceAm];
            lastSelectedLabelA.Background = null;
            AmphitheatrePlaces.Children[_lastSelectedPlaceAm] = lastSelectedLabelA;

            Label lastSelectedLabelB = (Label)BeletagePlaces.Children[_lastSelectedPlaceBl];
            lastSelectedLabelB.Background = null;
            BeletagePlaces.Children[_lastSelectedPlaceBl] = lastSelectedLabelB;
        }
    }
}
