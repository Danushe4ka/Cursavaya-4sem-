using Npgsql;
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
        static readonly DBConnectionManager instance = new DBConnectionManager();
        /// <summary>
        /// Поле строки подключения
        /// </summary>
        readonly string _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//qy5k--zhr8a98L
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
        public static DBConnectionManager GetInstance { get => instance; }
        /// <summary>
        /// Свойство для получения экземпляра подключения
        /// </summary>
        public NpgsqlConnection Connection { get { return _connection; } }
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
