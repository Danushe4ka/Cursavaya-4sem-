
namespace DBObjectsClassLibrary.Models.Tickets
{
    /// <summary>
    /// Интерфейс, определяющий стоимость билета
    /// </summary>
    interface ITicket
    {
        /// <summary>
        /// Метод получения стоимости билета
        /// </summary>
        /// <returns>Стоимость билета(тип double)</returns>
        double GetCost();
    }
}
