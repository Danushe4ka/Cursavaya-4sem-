﻿using Npgsql;
using System.Configuration;

namespace DBObjectsClassLibrary
{
    /// <summary>
    /// Класс-одиночка подключения к базе данных 
    /// </summary>
    public class DBConnectionManager
    {
        /// <summary>
        /// Поле, содержащее сылку на DBConnectionManager
        /// </summary>
        static readonly DBConnectionManager _instance = new DBConnectionManager();
        /// <summary>
        /// Поле строки подключения
        /// </summary>
        readonly string _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        /// <summary>
        /// Поле экземпляра подключения к базе данных
        /// </summary>
        readonly NpgsqlConnection _connection;
        /// <summary>
        /// Конструктор класса DBConnectionManager
        /// </summary>
        DBConnectionManager()
        {
            _connection = new NpgsqlConnection(_sqlCon);
            _connection.Open();
        }
        /// <summary>
        /// Свойство для получения ссылки на объект DBConnectionManager
        /// </summary>
        public static DBConnectionManager GetInstance { get => _instance; }
        /// <summary>
        /// Свойство для получения экземпляра подключения
        /// </summary>
        public NpgsqlConnection Connection { get => _connection; }
        /// <summary>
        /// Метод закрытия подключения
        /// </summary>
        public void CloseConnection()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
