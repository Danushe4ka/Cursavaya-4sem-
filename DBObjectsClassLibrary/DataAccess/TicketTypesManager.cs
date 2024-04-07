using DBObjectsClassLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Класс-менеджер типов билетов
    /// </summary>
    public class TicketTypesManager:CommonManager<TicketType>
    {
        public override List<TicketType> Read()
        {
            List<TicketType> ticketTypes = new List<TicketType>();
            _command.CommandText = "select * from TicketTypes order by TypeId;";
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    ticketTypes.Add(new TicketType(reader.GetInt16(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3)));
            reader.Close();
            return ticketTypes;
        }
        public override void Update(TicketType type)
        {
            string command = $"update TicketTypes set TypeCost={type.TypeCost.ToString().Replace(',','.')} ";
            command += $"where TypeId = {type.TypeId}";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
        public override void Create(TicketType obj)
        {
            throw new Exception("Операция запрещена!");
        }
        public override void Delete(TicketType obj)
        {
            throw new Exception("Операция запрещена!");
        }
    }
}
