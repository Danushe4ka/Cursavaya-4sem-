using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models.Tickets
{
    public class ParterTicket:Ticket
    {
        public ParterTicket(BaseUser user, Spectacle spectacle, double cost, int place) : base(user, spectacle, cost, place) { }
        public override string Type { get => "Партер"; }
    }
}
