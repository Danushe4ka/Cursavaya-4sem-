using DBObjectsClassLibrary.Models;
using System;
using System.Collections.Generic;

namespace DBObjectsClassLibrary.Utility
{
    /// <summary>
    /// Класс-одиночка корзины для билетов
    /// </summary>
    public class Bin
    {
        /// <summary>
        /// Поле, содержащее сылку на TicketBin
        /// </summary>
        static readonly Lazy<Bin> _instance = new Lazy<Bin>(() => new Bin(), true); //используем потокобезопасную lazy реализацию для предотвращения единовременного доступа к корзине
        /// <summary>
        /// Список билетов в корзине
        /// </summary>
        List<Ticket> _ticketList;
        /// <summary>
        /// Конструктор класса TicketBin
        /// </summary>
        public Bin()
        {
            _ticketList = new List<Ticket>();
        }
        /// <summary>
        /// Метод добавления билета в корзину
        /// </summary>
        /// <param name="ticket">Билет</param>
        public void AddToBin(Ticket ticket)
        { _ticketList.Add(ticket); }
        /// <summary>
        /// Метод удаления билета из корзины
        /// </summary>
        /// <param name="ticket">Билет</param>
        public void RemoveFromBin(Ticket ticket)
        { _ticketList.Remove(ticket); }
        /// <summary>
        /// Метод очистки корзины
        /// </summary>
        public void ClearBin()
        { _ticketList.Clear(); }
        /// <summary>
        /// Свойство для получения ссылки на объект TicketBin
        /// </summary>
        public static Bin GetInstance { get => _instance.Value; }
        public List<Ticket> GetBin {  get => _ticketList; }
    }
}
