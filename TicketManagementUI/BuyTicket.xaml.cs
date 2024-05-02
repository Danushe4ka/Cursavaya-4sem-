using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Tickets;
using DBObjectsClassLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        List<int> _parterFreePlaces;
        List<int> _parterSelectedPlaces = new List<int>();
        List<int> _amphitheatreFreePlaces;
        List<int> _amphitheatreSelectedPlaces = new List<int>();
        List<int> _beletageFreePlaces;
        List<int> _beletageSelectedPlaces = new List<int>();
        /// <summary>
        /// Конструктор класса-окна покупки билета
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="spectacle">Спектакль</param>
        public BuyTicket(BaseUser user, Spectacle spectacle)
        {
            _user = user;
            _spectacle = spectacle;
            _tickets = _ticketManager.Read();
            _parterFreePlaces = FreePlacesByType("Партер");
            _amphitheatreFreePlaces = FreePlacesByType("Амфитеатр");
            _beletageFreePlaces = FreePlacesByType("Бельэтаж");
            InitializeComponent();
            UpdateTheatrePlaces();
            if(Bin.GetInstance.GetBin.Count() > 0)
                UpdateTheatrePlacesByBin();
        }
        /// <summary>
        /// Кнопка перехода в окно покупки выбранных мест
        /// </summary>
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (int place in _parterSelectedPlaces)
                Bin.GetInstance.AddToBin(new ParterTicket(_user, _spectacle, _typesManager.GetTicketTypePrice("Партер"), place, _typesManager.GetTicketTypePlaceAmount("Партер")));
            foreach (int place in _amphitheatreSelectedPlaces)
                Bin.GetInstance.AddToBin(new AmphitheatreTicket(_user, _spectacle, _typesManager.GetTicketTypePrice("Амфитеатр"), place, _typesManager.GetTicketTypePlaceAmount("Амфитеатр")));
            foreach (int place in _beletageSelectedPlaces)
                Bin.GetInstance.AddToBin(new BeletageTicket(_user, _spectacle, _typesManager.GetTicketTypePrice("Бельэтаж"), place, _typesManager.GetTicketTypePlaceAmount("Бельэтаж")));
            this.Close();
        }
        /// <summary>
        /// Метод получения свободных мест по типу в виде списка целых чисел
        /// </summary>
        /// <param name="ticketType">Наименование типа</param>
        /// <returns>Список целых чисел(свободных мест)</returns>
        private List<int> FreePlacesByType(string ticketType)
        {
            int amountOfPlaces = _typesManager.GetTicketTypePlaceAmount(ticketType);
            List<int> places = new List<int>();
            for (int i = 1; i <= amountOfPlaces; i++)
            {
                bool isReq = true;
                foreach (Ticket ticket in _tickets.Where(t => t.Type == ticketType && t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i))
                    isReq = false;
                if (isReq)
                    places.Add(i);
            }
            return places;
        }
        /// <summary>
        /// Метод для сокрытия уже купленных мест
        /// </summary>
        private void UpdateTheatrePlaces()
        {
            for (int i = 1; i <= 25; i++)
                foreach (Ticket ticket in _tickets.Where(t => t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i && t.Type == "Партер" && ParterPlaces.Children[i - 1] != null))
                {
                    ParterPlaces.Children[i - 1].Opacity = 0.2;
                    ParterPlaces.Children[i - 1].IsEnabled = false;
                }
            for (int i = 1; i <= 10; i++)
                foreach (Ticket ticket in _tickets.Where(t => t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i))
                {
                    if (AmphitheatrePlaces.Children[i - 1] != null && ticket.Type == "Амфитеатр")
                    {
                        AmphitheatrePlaces.Children[i - 1].Opacity = 0.2;
                        AmphitheatrePlaces.Children[i - 1].IsEnabled = false;
                    }
                    else if (BeletagePlaces.Children[i - 1] != null && ticket.Type == "Бельэтаж")
                    {
                        BeletagePlaces.Children[i - 1].Opacity = 0.2;
                        BeletagePlaces.Children[i - 1].IsEnabled = false;
                    }
                }
        }
        /// <summary>
        /// Метод для сокрытия добавленных в корзину мест
        /// </summary>
        private void UpdateTheatrePlacesByBin()
        {
            for (int i = 1; i <= 25; i++)
                foreach (Ticket ticket in Bin.GetInstance.GetBin.Where(t => t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i && t.Type == "Партер" && ParterPlaces.Children[i - 1] != null))
                {
                    ParterPlaces.Children[i - 1].IsEnabled = false;
                }
            for (int i = 1; i <= 10; i++)
                foreach (Ticket ticket in Bin.GetInstance.GetBin.Where(t => t.Spectacle.SpectacleDate == _spectacle.SpectacleDate && t.Place == i))
                {
                    if (AmphitheatrePlaces.Children[i - 1] != null && ticket.Type == "Амфитеатр")
                    {
                        AmphitheatrePlaces.Children[i - 1].IsEnabled = false;
                    }
                    else if (BeletagePlaces.Children[i - 1] != null && ticket.Type == "Бельэтаж")
                    {
                        BeletagePlaces.Children[i - 1].IsEnabled = false;
                    }
                }
        }
        /// <summary>
        /// Метод выбора места типа Партер
        /// </summary>
        private void ParterButton_Click(object sender, RoutedEventArgs e)
        {
            int place = Convert.ToInt32(((Button)sender).Content);
            if (_parterFreePlaces.Contains(place) && !_parterSelectedPlaces.Contains(place))
            {
                ((Button)sender).Background = Brushes.Aquamarine;
                _parterSelectedPlaces.Add(place);
                BuyButton.IsEnabled = true;
            }
            else if(_parterFreePlaces.Contains(place) && _parterSelectedPlaces.Contains(place))
            {
                ((Button)sender).Background = Brushes.White;
                _parterSelectedPlaces.Remove(place);
                if (!IsAnyTicketSelected())
                    BuyButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// Метод выбора места типа Амфитеатр
        /// </summary>
        private void AmphyButton_Click(object sender, RoutedEventArgs e)
        {
            int place = Convert.ToInt32(((Button)sender).Content);
            if (_amphitheatreFreePlaces.Contains(place) && !_amphitheatreSelectedPlaces.Contains(place))
            {
                ((Button)sender).Background = Brushes.CornflowerBlue;
                _amphitheatreSelectedPlaces.Add(place);
                BuyButton.IsEnabled = true;
            }
            else if(_amphitheatreFreePlaces.Contains(place) && _amphitheatreSelectedPlaces.Contains(place))
            {
                ((Button)sender).Background = Brushes.White;
                _amphitheatreSelectedPlaces.Remove(place);
                if(!IsAnyTicketSelected())
                    BuyButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// Метод выбора места типа Бельэтаж
        /// </summary>
        private void BeletageButton_Click(object sender, RoutedEventArgs e)
        {
            int place = Convert.ToInt32(((Button)sender).Content);
            if (_beletageFreePlaces.Contains(place) && !_beletageSelectedPlaces.Contains(place))
            {
                ((Button)sender).Background = Brushes.Blue;
                _beletageSelectedPlaces.Add(place);
                BuyButton.IsEnabled = true;
            }
            else if (_beletageFreePlaces.Contains(place) && _beletageSelectedPlaces.Contains(place))
            {
                ((Button)sender).Background = Brushes.White;
                _beletageSelectedPlaces.Remove(place);
                if (!IsAnyTicketSelected())
                    BuyButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// Определяет, выбран ли хоть один билет
        /// </summary>
        /// <returns>False,если ни одного места не выбрано.Иначе True</returns>
        private bool IsAnyTicketSelected()
        {
            bool isSelected = true;
            if (_amphitheatreSelectedPlaces.Count < 1 && _parterSelectedPlaces.Count < 1 && _beletageSelectedPlaces.Count < 1)
                isSelected = false;
            return isSelected;
        }
    }
}
