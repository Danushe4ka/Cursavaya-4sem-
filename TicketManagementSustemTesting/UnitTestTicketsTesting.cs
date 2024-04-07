using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestTicketsTesting
    {
        TicketsManager ticketsManager = new TicketsManager();
        Ticket ticket = new Ticket(1, 1, 1, 1, 1);
        [TestMethod]
        public void TestRead()
        {
            ticketsManager.Create(ticket);
            var tickets = ticketsManager.Read();
            Assert.AreEqual(ticketsManager.Read()[0].SpectacleID, 1);
            ticketsManager.Delete(new Ticket(tickets[tickets.Count - 1].TicketId, 1, 1, 1, 1));
        }
        [TestMethod]
        public void TestCreate()
        {
            ticketsManager.Create(ticket);
            var tickets = ticketsManager.Read();
            Assert.AreEqual(tickets[tickets.Count - 1].UserId, 1);
            ticketsManager.Delete(new Ticket(tickets[tickets.Count - 1].TicketId, 1, 1, 1, 1));
        }
        [TestMethod]
        public void TestUpdate()
        {
            ticketsManager.Create(ticket);
            var tickets = ticketsManager.Read();
            int ticketId = tickets[tickets.Count - 1].TicketId;
            ticketsManager.Update(new Ticket(ticketId, 2, 1, 1, 1));
            Assert.IsTrue(ticketsManager.Read()[tickets.Count - 1].UserId == 2);
            ticketsManager.Delete(new Ticket(tickets[tickets.Count - 1].TicketId, 1, 1, 1, 1));
        }
        [TestMethod]
        public void TestDelete()
        {
            ticketsManager.Create(ticket);
            var tickets = ticketsManager.Read();

            int ticketId = tickets[tickets.Count - 1].TicketId;
            ticketsManager.Delete(new Ticket(ticketId, 1, 1, 1, 1));
            Assert.IsTrue(ticketsManager.Read().Count == tickets.Count - 1);
        }
    }
}
