using Npgsql;
using System;
using System.Collections.Generic;

namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Класс-менеджер типов билетов
    /// </summary>
    public class TicketTypesManager
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
        /// Конструктор класса TicketTypesManager
        /// </summary>
        public TicketTypesManager()
        {
            _connection = DBConnectionManager.GetInstance.Connection;
            _command = new NpgsqlCommand
            {
                Connection = _connection,
                CommandType = System.Data.CommandType.Text
            };
        }
        /// <summary>
        /// Метод получения наименований типов билетов
        /// </summary>
        /// <returns>Наименования типов билетов в виде строки</returns>
        public List<string> GetTypeNames()
        {
            _command.Parameters.Clear();
            List<string> types = new List<string>();
            string command = $"select TypeName from TicketTypes order by @typeid;";
            _command.CommandText = command;
            NpgsqlParameter typeParam = new NpgsqlParameter("@typeid", "Typeid");
            _command.Parameters.Add(typeParam);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    types.Add(reader.GetString(0));
            reader.Close();
            return types;
        }
        /// <summary>
        /// Метод изменения цены на тип билета
        /// </summary>
        /// <param name="typeName">Наименование типа</param>
        /// <param name="price">Новая стоимость</param>
        public void ChangeTicketTypePrice(string typeName, double price)
        {
            _command.Parameters.Clear();
            string command = $"update TicketTypes set TypeCost={price.ToString().Replace(',', '.')} ";
            command += $"where TypeName = @nameParam";
            _command.CommandText = command;
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam", typeName);
            _command.Parameters.Add(nameParam);
            _command.ExecuteNonQuery();
        }
        /// <summary>
        /// Метод получения количества мест билета по типу
        /// </summary>
        /// <param name="typeName">Наименование типа</param>
        /// <returns>Количество мест в виде целого числа</returns>
        public int GetTicketTypePlaceAmount(string typeName)
        {
            _command.Parameters.Clear();
            string command = $"select GetTicketTypePlaceAmount('{typeName}')";
            _command.CommandText= command;
            int typeAmount = Convert.ToInt32(_command.ExecuteScalar());
            return typeAmount;
        }
        /// <summary>
        /// Метод получения цены билета по типу
        /// </summary>
        /// <param name="typeName">Наименование типа</param>
        /// <returns>Цену на тип в виде вещественного числа</returns>
        public double GetTicketTypePrice(string typeName)
        {
            _command.Parameters.Clear();
            string command = $"select GetTicketTypePlacePrice('{typeName}')";
            _command.CommandText = command;
            double typeAmount = Convert.ToDouble(_command.ExecuteScalar());
            return typeAmount;
        }
    }
}
