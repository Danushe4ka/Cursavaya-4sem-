using Npgsql;
using System.Collections.Generic;

namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Асбстрактный класс менеджера CRUD операций для базы данных
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public abstract class CommonManager<T> : ICreate<T>, IDelete<T>, IRead<T>, IUpdate<T>
    {
        /// <summary>
        /// Поле экземпляра подключения
        /// </summary>
        readonly NpgsqlConnection _connection;
        /// <summary>
        /// Поле экземпляра комманды
        /// </summary>
        protected NpgsqlCommand _command;
        /// <summary>
        /// Конструктор класса CommonManager
        /// </summary>
        public CommonManager()
        {
            _connection = DBConnectionManager.GetInstance.Connection;
            _command = new NpgsqlCommand
            {
                Connection = _connection,
                CommandType = System.Data.CommandType.Text
            };
        }
        /// <summary>
        /// Метод для чтения объектов базы данных в список
        /// </summary>
        /// <returns>Список объектов таблицы</returns>
        public abstract List<T> Read();
        /// <summary>
        /// Метод для добавления объекта в базу данных
        /// </summary>
        /// <param name="obj">Объект для добавления</param>
        public abstract void Create(T obj);
        /// <summary>
        /// Метод удаления объекта из базы данных
        /// </summary>
        /// <param name="obj">Объект для удаления</param>
        public abstract void Delete(T obj);
        /// <summary>
        /// Метод обновления объекта базы данных(по id)
        /// </summary>
        /// <param name="obj">Объект для обновления</param>
        public abstract void Update(T obj);
    }
}
