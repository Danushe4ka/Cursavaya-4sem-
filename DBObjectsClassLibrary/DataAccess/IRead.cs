using System.Collections.Generic;

namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Интерфейс чтения объектов базы данных
    /// </summary>
    /// <typeparam name="T">Тип объектов</typeparam>
    interface IRead<T>
    {
        /// <summary>
        /// Чтение объектов
        /// </summary>
        /// <returns>Список объектов</returns>
        List<T> Read();
    }
}
