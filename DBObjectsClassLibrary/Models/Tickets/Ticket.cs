using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс билета
    /// </summary>
    public abstract class Ticket: ITicket
    {
        BaseUser _user;
        Spectacle _spectacle;
        double _cost;
        int _place;
        bool _isConfirmed;
        /// <summary>
        /// Конструктор класса Ticket
        /// </summary>
        /// <param name="user">Владелец билета</param>
        /// <param name="spectacle">Спектакля</param>
        /// <param name="cost">Цена билета</param>
        /// <param name="place">Место</param>
        /// <param name="isConfirmed">Оформлен ли билет</param>
        public Ticket(BaseUser user,Spectacle spectacle, double cost, int place, bool isConfirmed = false)
        {
            _user = user;
            _spectacle = spectacle;
            _cost = cost;
            _place = place;
            _isConfirmed = isConfirmed;
        }
        /// <summary>
        /// Свойство для получения владельца
        /// </summary>
        public BaseUser User { get => _user; }
        /// <summary>
        /// Свойство для получения спектакля
        /// </summary>
        public Spectacle Spectacle {  get => _spectacle; }
        /// <summary>
        /// Свойство для получения номера места в зале
        /// </summary>
        public int Place { get => _place; }
        /// <summary>
        /// Свойство для получения цены билеты
        /// </summary>
        public double Cost { get => GetCost(); }
        /// <summary>
        /// Свойство для получения информации, оформлен ли билет
        /// </summary>
        public bool IsConfirmed { get => _isConfirmed; set => _isConfirmed = value; }
        public string UserName { get => _user.UserName; }
        /// <summary>
        /// Свойство для получения типа билета
        /// </summary>
        public abstract string Type {  get; }
        public abstract int PlaceAmount { get;protected set; }
        public string SpectacleInformation { get => GetSpectacleInfo(); }
        string GetSpectacleInfo()
        {
            string info;
            info = $"'{_spectacle.SpectacleName}' жанр - <{_spectacle.Genre}> автор - <{_spectacle.SpectacleAuthor}> дата показа: {_spectacle.SpectacleDate}";
            return info;
        }
        public virtual double GetCost()
        {
            return _cost;
        }
    }
}
