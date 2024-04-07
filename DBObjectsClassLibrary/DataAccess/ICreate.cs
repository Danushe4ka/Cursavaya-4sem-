using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
