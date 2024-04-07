using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
