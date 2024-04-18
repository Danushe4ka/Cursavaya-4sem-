using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Tickets;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
            var users = new UsersManager().Read();
            var spectacles = new SpectaclesManager().Read();
            _command.CommandText = "select * from TicketView order by @ticketId;";
            NpgsqlParameter ticketParam = new NpgsqlParameter("@ticketId", "TicketId");
            _command.Parameters.Add(ticketParam);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    var user = users.Find(t => t.UserName == reader.GetString(0));
                    var spectacle = spectacles.Find(s => s.SpectacleName == reader.GetString(1));
                    switch (reader.GetString(2))
                    {
                        case "Партер":
                            tickets.Add(new ParterTicket(user, spectacle, Convert.ToDouble(reader.GetDecimal(3)),reader.GetInt32(4)));
                            break;
                        case "Амфитеатр":
                            tickets.Add(new AmphitheatreTicket(user, spectacle, Convert.ToDouble(reader.GetDecimal(3)), reader.GetInt32(4)));
                            break;
                        case "Бельэтаж":
                            tickets.Add(new BeletageTicket(user, spectacle, Convert.ToDouble(reader.GetDecimal(3)), reader.GetInt32(4)));
                            break;
                        default:
                            throw new Exception("DB reader malfunction!");
                    }
                }
            reader.Close();
            return tickets;
        }
        public override void Create(Ticket ticket)
        {
            _command.Parameters.Clear();
            string command = $"select GetUserId('{ticket.User.UserName}')";
            _command.CommandText = command;
            int userId = Convert.ToInt32(_command.ExecuteScalar());
            command = $"select GetUserId('{ticket.Spectacle.SpectacleDate}')";
            _command.CommandText = command;
            int spectacleId = Convert.ToInt32(_command.ExecuteScalar());
            command = $"insert into Tickets(UserId, SpectacleId, TypeId, Place) values(@userParam, @specParam, @typeParam, @placeParam);";
            _command.CommandText = command;
            NpgsqlParameter userParam = new NpgsqlParameter("@userParam", userId);
            NpgsqlParameter specParam = new NpgsqlParameter("@specParam", spectacleId);
            NpgsqlParameter typeParam;
            switch(ticket.Type)
            {
                case "Партер":
                    typeParam = new NpgsqlParameter("@typeParam", 1);
                    break;
                case "Амфитеатр":
                    typeParam = new NpgsqlParameter("@typeParam", 2);
                    break;
                case "Бельэтаж":
                    typeParam = new NpgsqlParameter("@typeParam", 3);
                    break;
                default:
                    throw new Exception("DB reader malfunction!");
            }
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
            string command = $"select GetUserId('{ticket.User.UserName}')";
            _command.CommandText = command;
            int userId = Convert.ToInt32(_command.ExecuteScalar());
            command = $"select GetUserId('{ticket.Spectacle.SpectacleDate}')";
            _command.CommandText = command;
            int spectacleId = Convert.ToInt32(_command.ExecuteScalar());
            command = $"update Tickets set IsTicketConfirmed='{ticket.IsConfirmed}' ";
            command += $"where UserId = @userParam and SpectacleId = @specParam and Typeid = @typeParam and Place = @placeParam";
            _command.CommandText = command;
            NpgsqlParameter userParam = new NpgsqlParameter("@userParam", userId);
            NpgsqlParameter specParam = new NpgsqlParameter("@specParam", spectacleId);
            NpgsqlParameter typeParam;
            switch (ticket.Type)
            {
                case "Партер":
                    typeParam = new NpgsqlParameter("@typeParam", 1);
                    break;
                case "Амфитеатр":
                    typeParam = new NpgsqlParameter("@typeParam", 2);
                    break;
                case "Бельэтаж":
                    typeParam = new NpgsqlParameter("@typeParam", 3);
                    break;
                default:
                    throw new Exception("DB reader malfunction!");
            }
            NpgsqlParameter placeParam = new NpgsqlParameter("@placeParam", ticket.Place);
            _command.Parameters.Add(userParam);
            _command.Parameters.Add(specParam);
            _command.Parameters.Add(typeParam);
            _command.Parameters.Add(placeParam);
            _command.ExecuteNonQuery();
        }
        public override void Delete(Ticket ticket)
        {
            _command.Parameters.Clear();
            string command = $"select GetUserId('{ticket.User.UserName}')";
            _command.CommandText = command;
            int userId = Convert.ToInt32(_command.ExecuteScalar());
            command = $"select GetUserId('{ticket.Spectacle.SpectacleDate}')";
            _command.CommandText = command;
            int spectacleId = Convert.ToInt32(_command.ExecuteScalar());
            command = $"delete from Tickets ";
            command += $"where UserId = @userParam and SpectacleId = @specParam and Typeid = @typeParam and Place = @placeParam";
            _command.CommandText = command;
            NpgsqlParameter userParam = new NpgsqlParameter("@userParam", userId);
            NpgsqlParameter specParam = new NpgsqlParameter("@specParam", spectacleId);
            NpgsqlParameter typeParam;
            switch (ticket.Type)
            {
                case "Партер":
                    typeParam = new NpgsqlParameter("@typeParam", 1);
                    break;
                case "Амфитеатр":
                    typeParam = new NpgsqlParameter("@typeParam", 2);
                    break;
                case "Бельэтаж":
                    typeParam = new NpgsqlParameter("@typeParam", 3);
                    break;
                default:
                    throw new Exception("DB reader malfunction!");
            }
            NpgsqlParameter placeParam = new NpgsqlParameter("@placeParam", ticket.Place);
            _command.Parameters.Add(userParam);
            _command.Parameters.Add(specParam);
            _command.Parameters.Add(typeParam);
            _command.Parameters.Add(placeParam);
            _command.ExecuteNonQuery();
        }
        public void ChangeTicketTypePrice(string typeName,double price)
        {
            _command.Parameters.Clear();
            string command = $"update TicketTypes set TypeCost={price.ToString().Replace(',','.')} ";
            command += $"where TypeName = @nameParam";
            _command.CommandText = command;
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam", typeName);
            _command.Parameters.Add(nameParam);
            _command.ExecuteNonQuery();
        }
    }
}
