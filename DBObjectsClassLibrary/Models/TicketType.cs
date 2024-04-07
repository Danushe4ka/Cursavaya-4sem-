using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс типа билета
    /// </summary>
    public class TicketType
    {
        short _typeId;
        string _typeName;
        int _typeAmount;
        double _typeCost;
        /// <summary>
        /// Конструктор класса TicketType
        /// </summary>
        /// <param name="typeId">Id типа</param>
        /// <param name="typeName">Имя типа</param>
        /// <param name="typeAmount">Количество билетов типа</param>
        /// <param name="typeCost">Цена данного типа</param>
        public TicketType(short typeId, string typeName, int typeAmount, double typeCost)
        {
            _typeId = typeId;
            _typeName = typeName;
            _typeAmount = typeAmount;
            _typeCost = typeCost;
        }
        /// <summary>
        /// Свойство для получения Id типа
        /// </summary>
        public short TypeId { get => _typeId; }
        /// <summary>
        /// Свойство для получения имени типа
        /// </summary>
        public string TypeName { get => _typeName; }
        /// <summary>
        /// Свойство для получения количества билетов типа
        /// </summary>
        public int TypeAmount { get => _typeAmount; }
        /// <summary>
        /// Свойство для получения цены данного типа
        /// </summary>
        public double TypeCost { get => _typeCost; }
    }
}
