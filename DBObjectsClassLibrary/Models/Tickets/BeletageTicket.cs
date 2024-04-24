
namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс билета типа бельэтаж
    /// </summary>
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
