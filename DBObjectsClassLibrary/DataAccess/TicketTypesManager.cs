using DBObjectsClassLibrary.Models.Tickets;
using DBObjectsClassLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.DataAccess
{
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
            _command = new NpgsqlCommand();
            _command.Connection = _connection;
            _command.CommandType = System.Data.CommandType.Text;
        }
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
        public int GetTicketTypePlaceAmount(string typeName)
        {
            _command.Parameters.Clear();
            string command = $"select GetTicketTypePlaceAmount('{typeName}')";
            _command.CommandText= command;
            int typeAmount = Convert.ToInt32(_command.ExecuteScalar());
            return typeAmount;
        }
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
