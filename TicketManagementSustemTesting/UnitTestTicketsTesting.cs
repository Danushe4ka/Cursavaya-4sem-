using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestTicketsTesting
    {
        NpgsqlCommand _cmd = new NpgsqlCommand();
        TicketsManager _ticketsManager = new TicketsManager();
        Ticket _newTicket = new BeletageTicket(new UsersManager().Read()[0], new SpectaclesManager().Read()[0], new TicketTypesManager().GetTicketTypePrice("Бельэтаж"), 1, new TicketTypesManager().GetTicketTypePlaceAmount("Бельэтаж"));
        [TestMethod]
        public void TestRead()
        {
            _cmd.Connection = DBConnectionManager.GetInstance.Connection;
            _cmd.CommandText = "select count(*) from Tickets";
            int count = Convert.ToInt32(_cmd.ExecuteScalar());
            Assert.AreEqual(_ticketsManager.Read().Count, count);
        }
        [TestMethod]
        public void TestCreate()
        {
            _ticketsManager.Create(_newTicket);
            var tickets = _ticketsManager.Read();
            Assert.AreEqual(tickets[tickets.Count - 1].Type, "Бельэтаж");
            _ticketsManager.Delete(_newTicket);
        }
        [TestMethod]
        public void TestUpdate()
        {
            _ticketsManager.Create(_newTicket);
            var tickets = _ticketsManager.Read();
            var nnticket = _newTicket;
            nnticket.IsConfirmed = true;
            _ticketsManager.Update(nnticket);
            Assert.IsTrue(_ticketsManager.Read()[tickets.Count - 1].Type == "Бельэтаж");
            _ticketsManager.Delete(nnticket);
        }
        [TestMethod]
        public void TestDelete()
        {
            _ticketsManager.Create(_newTicket);
            var tickets = _ticketsManager.Read();
            _ticketsManager.Delete(_newTicket);
            Assert.IsTrue(_ticketsManager.Read().Count == tickets.Count - 1);
        }
    }
}
