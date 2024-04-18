using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models.Tickets
{
    public class AmphitheatreTicket:Ticket
    {
        public AmphitheatreTicket(BaseUser user, Spectacle spectacle, double cost, int place) : base(user, spectacle, cost, place) { }
        public override string Type { get => "Амфитеатр"; }
    }
}
