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
            _command.Parameters.Clear();
            List<Ticket> tickets = new List<Ticket>();
            _command.CommandText = "select * from Tickets order by @ticketId;";
            NpgsqlParameter ticketParam = new NpgsqlParameter("@ticketId", "TicketId");
            _command.Parameters.Add(ticketParam);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    tickets.Add(new Ticket(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt16(3), reader.GetInt32(4), reader.GetBoolean(5)));
            reader.Close();
            return tickets;
        }
        public override void Create(Ticket ticket)
        {
            _command.Parameters.Clear();
            string command = $"insert into Tickets(UserId, SpectacleId, TypeId, Place) values(@userParam, @specParam, @typeParam, @placeParam);";
            _command.CommandText = command;
            NpgsqlParameter userParam = new NpgsqlParameter("@userParam", ticket.UserId);
            NpgsqlParameter specParam = new NpgsqlParameter("@specParam", ticket.SpectacleID);
            NpgsqlParameter typeParam = new NpgsqlParameter("@typeParam", ticket.TypeId);
            NpgsqlParameter placeParam = new NpgsqlParameter("@placeParam", ticket.Place);
            _command.Parameters.Add(userParam);
            _command.Parameters.Add(specParam);
            _command.Parameters.Add(typeParam);
            _command.Parameters.Add(placeParam);
            _command.ExecuteNonQuery();
        }
        public override void Update(Ticket ticket)
        {
            _command.Parameters.Clear();
            string command = $"update Tickets set UserId=@userParam, SpectacleId=@specParam,TypeId=@typeParam,Place=@placeParam, IsTicketConfirmed='{ticket.IsConfirmed}'";
            command += $"where TicketId = @idParam";
            _command.CommandText = command;
            NpgsqlParameter userParam = new NpgsqlParameter("@userParam", ticket.UserId);
            NpgsqlParameter specParam = new NpgsqlParameter("@specParam", ticket.SpectacleID);
            NpgsqlParameter typeParam = new NpgsqlParameter("@typeParam", ticket.TypeId);
            NpgsqlParameter placeParam = new NpgsqlParameter("@placeParam", ticket.Place);
            NpgsqlParameter idParam = new NpgsqlParameter("@idParam", ticket.TicketId);
            _command.Parameters.Add(userParam);
            _command.Parameters.Add(specParam);
            _command.Parameters.Add(typeParam);
            _command.Parameters.Add(placeParam);
            _command.Parameters.Add(idParam);
            _command.ExecuteNonQuery();
        }
        public override void Delete(Ticket ticket)
        {
            _command.Parameters.Clear();
            string command = $"delete from Tickets where TicketId = @idParam;";
            _command.CommandText = command;
            NpgsqlParameter idParam = new NpgsqlParameter("@idParam", ticket.TicketId);
            _command.Parameters.Add(idParam);
            _command.ExecuteNonQuery();
        }
    }
}
