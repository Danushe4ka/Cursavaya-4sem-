
namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Интерфейс удаления объекта базы данных
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    interface IDelete<T>
    {
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        void Delete(T obj);
    }
}
