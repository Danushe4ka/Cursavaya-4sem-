using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
