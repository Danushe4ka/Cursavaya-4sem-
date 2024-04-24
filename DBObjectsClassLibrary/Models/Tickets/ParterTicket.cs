
namespace DBObjectsClassLibrary.Models.Tickets
{
    /// <summary>
    /// Класс билета типа Партер
    /// </summary>
    public class ParterTicket:Ticket
    {
        public ParterTicket(BaseUser user, Spectacle spectacle, double cost, int place, int amount, bool isConfirmed = false) : base(user, spectacle, cost, place, isConfirmed)
        {
            PlaceAmount = amount;
        }
        public override string Type { get => "Партер"; }
        public override int PlaceAmount { get; protected set; }
        public override double GetCost()
        {
            return base.GetCost() * 2.0;
        }
    }
}
