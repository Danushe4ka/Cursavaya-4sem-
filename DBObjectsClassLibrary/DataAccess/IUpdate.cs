
namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Интерфейс обновления объекта базы данных
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    interface IUpdate<T>
    {
        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        void Update(T obj);
    }
}
