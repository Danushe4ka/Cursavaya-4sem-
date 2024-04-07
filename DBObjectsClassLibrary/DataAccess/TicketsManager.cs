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
    /// Класс-менеджер билетов
    /// </summary>
    public class TicketsManager:CommonManager<Ticket>
    {
        public override List<Ticket> Read()
        {
            List<Ticket> tickets = new List<Ticket>();
            _command.CommandText = "select * from Tickets order by TicketId;";
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    tickets.Add(new Ticket(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt16(3), reader.GetInt32(4), reader.GetBoolean(5)));
            reader.Close();
            return tickets;
        }
        public override void Create(Ticket ticket)
        {
            string command = $"insert into Tickets(UserId, SpectacleId, TypeId, Place) values({ticket.UserId}, {ticket.SpectacleID}, {ticket.TypeId}, {ticket.Place});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
        public override void Update(Ticket ticket)
        {
            string command = $"update Tickets set UserId={ticket.UserId}, SpectacleId={ticket.SpectacleID},TypeId={ticket.TypeId},Place={ticket.Place}, IsTicketConfirmed='{ticket.IsConfirmed}'";
            command += $"where TicketId = {ticket.TicketId}";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
        public override void Delete(Ticket ticket)
        {
            string command = $"delete from Tickets where TicketId = {ticket.TicketId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
    }
}
