using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
