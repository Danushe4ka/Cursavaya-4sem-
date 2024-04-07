using DBObjectsClassLibrary.DataAccess;
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
    public class Ticket
    {
        int _ticketId;
        int _userId;
        int _spectacleId;
        short _typeId;
        int _place;
        bool _isConfirmed;
        /// <summary>
        /// Конструктор класса Ticket
        /// </summary>
        /// <param name="ticketId">Id билета</param>
        /// <param name="userId">Id владельца билета</param>
        /// <param name="spectacleId">Id спектакля</param>
        /// <param name="typeId">Id типа билета</param>
        /// <param name="place">Место</param>
        /// <param name="isConfirmed">Оформлен ли билет</param>
        public Ticket(int ticketId, int userId,int spectacleId, short typeId, int place, bool isConfirmed = false)
        {
            _ticketId = ticketId;
            _userId = userId;
            _spectacleId = spectacleId;
            _typeId = typeId;
            _place = place;
            _isConfirmed = isConfirmed;
        }
        /// <summary>
        /// Свойство для получения Id билета
        /// </summary>
        public int TicketId { get => _ticketId; }
        /// <summary>
        /// Свойство для получения Id покупателя
        /// </summary>
        public int UserId { get => _userId; }
        /// <summary>
        /// Свойство для получения Id спектакля
        /// </summary>
        public int SpectacleID {  get => _spectacleId; }
        /// <summary>
        /// Свойство для получения Id типа билета
        /// </summary>
        public short TypeId { get => _typeId; }
        /// <summary>
        /// Свойство для получения номера места в зале
        /// </summary>
        public int Place { get => _place; }
        /// <summary>
        /// Свойство для получения информации, оформлен ли билет
        /// </summary>
        public bool IsConfirmed { get => _isConfirmed; set => _isConfirmed = value; }
        /// <summary>
        /// Свойство для получения имени покупателя
        /// </summary>
        public string UserName { get => userName(); }
        /// <summary>
        /// Свойство для получения типа билета
        /// </summary>
        public string TypeName { get => typeName(); }
        /// <summary>
        /// Свойство для получения названия спектакля
        /// </summary>
        public string SpectacleName { get => spectacleName(); }
        /// <summary>
        /// Свойство для получения дополнительной информации о спектакле
        /// </summary>
        public string SpectacleAdditionalInformation { get => spectacleAdditionalInformation(); }
        /// <summary>
        /// Метод получения имени пользователя по Id
        /// </summary>
        /// <returns>Имя пользователя в виде строки</returns>
        string userName()
        {
            string name = null;
            UsersManager sm = new UsersManager();
            List<BaseUser> users = sm.Read();
            foreach(BaseUser user in users)
                if(user.UserId == _userId)
                    name = user.UserName;
            return name;
        }
        /// <summary>
        /// Метод получения названия спектакля по Id
        /// </summary>
        /// <returns>Название спектакля в виде строки</returns>
        string spectacleName()
        {
            string name = null;
            SpectaclesManager sm = new SpectaclesManager();
            List<Spectacle> spectacles = sm.Read();
            foreach (Spectacle spec in spectacles)
                if (spec.SpectacleId == _spectacleId)
                    name = spec.SpectacleName;
            return name;
        }
        /// <summary>
        /// Метод получения имени типа билета по Id
        /// </summary>
        /// <returns>Имя типа билета в виде строки</returns>
        string typeName()
        {
            string name = null;
            TicketTypesManager sm = new TicketTypesManager();
            List<TicketType> types = sm.Read();
            foreach(TicketType type in types)
                if(type.TypeId == _typeId)
                    name = type.TypeName;
            return name;
        }
        /// <summary>
        /// Метод для получения дополнительной информации о спектакле по Id
        /// </summary>
        /// <returns>Вся дополнительная информация о спектакле в виде одной строки</returns>
        string spectacleAdditionalInformation()
        {
            string info = null;
            SpectaclesManager sm = new SpectaclesManager();
            List<Spectacle> spectacles = sm.Read();
            foreach (Spectacle spec in spectacles)
                if (spec.SpectacleId == _spectacleId)
                    info = "Автор: " + spec.SpectacleAuthor + "  Жанр: " + spec.SpectacleGenre + "  Дата показа: " + spec.SpectacleDate.ToShortDateString() + "  Время показа: " + spec.SpectacleDate.TimeOfDay;
            return info;
        }
    }
}
