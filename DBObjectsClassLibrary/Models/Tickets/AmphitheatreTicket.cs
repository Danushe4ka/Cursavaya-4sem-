
namespace DBObjectsClassLibrary.Models.Tickets
{
    /// <summary>
    /// Класс билета типа амфитеатр
    /// </summary>
    public class AmphitheatreTicket:Ticket
    {
        public AmphitheatreTicket(BaseUser user, Spectacle spectacle, double cost, int place, int amount, bool isConfirmed = false) : base(user, spectacle, cost, place, isConfirmed)
        {
            PlaceAmount = amount;
        }
        public override string Type { get => "Амфитеатр"; }
        public override int PlaceAmount { get; protected set; }
        public override double GetCost()
        {
            return base.GetCost() * 1.5;
        }
    }
}
