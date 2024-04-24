
namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Интерфейс добавления объекта в базу данных
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    interface ICreate<T>
    {
        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        void Create(T obj);
    }
}
