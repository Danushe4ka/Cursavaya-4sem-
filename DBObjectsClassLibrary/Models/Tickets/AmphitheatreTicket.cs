using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models.Tickets
{
    public class AmphitheatreTicket:Ticket
    {
        public AmphitheatreTicket(BaseUser user, Spectacle spectacle, double cost, int place, int amount, bool isConfirmed = false) : base(user, spectacle, cost, place, isConfirmed)
        {
            PlaceAmount = amount;
        }
        public override string Type { get => "Амфитеатр"; }
        public override int PlaceAmount { get; protected set; }
    }
}
