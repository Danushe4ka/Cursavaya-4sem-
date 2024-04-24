using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    public class BeletageTicket:Ticket
    {
        public BeletageTicket(BaseUser user, Spectacle spectacle, double cost, int place,int amount, bool isConfirmed = false) :base(user, spectacle, cost, place, isConfirmed)
        {
            PlaceAmount = amount;
        }
        public override string Type { get => "Бельэтаж"; }
        public override int PlaceAmount { get; protected set; }
        public override double GetCost()
        {
            return base.GetCost() * 1.0;
        }
    }
}
