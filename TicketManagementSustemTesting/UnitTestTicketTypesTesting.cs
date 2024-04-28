using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestTicketTypesTesting
    {
        NpgsqlCommand _cmd = new NpgsqlCommand();
        TicketTypesManager _ticketTypesManager = new TicketTypesManager();
        [TestMethod]
        public void TestTypeNames()
        {
            Assert.AreEqual(_ticketTypesManager.GetTypeNames().Count, 3);
        }
        [TestMethod]
        public void TestTypePrice()
        {
            _cmd.Connection = DBConnectionManager.GetInstance.Connection;
            _cmd.CommandText = "select TypeCost from TicketTypes where TypeId = 1";
            double price = Convert.ToDouble(_cmd.ExecuteScalar());
            Assert.AreEqual(_ticketTypesManager.GetTicketTypePrice("Партер"), price);
        }
        [TestMethod]
        public void TestTypeAmount()
        {
            _cmd.Connection = DBConnectionManager.GetInstance.Connection;
            _cmd.CommandText = "select TypeAmount from TicketTypes where TypeId = 1";
            int amount = Convert.ToInt32(_cmd.ExecuteScalar());
            Assert.AreEqual(_ticketTypesManager.GetTicketTypePlaceAmount("Партер"), amount);
        }
        [TestMethod]
        public void TestChangeTypePrice()
        {
            double oldPrice = _ticketTypesManager.GetTicketTypePrice("Партер");
            double price = 1400.11;
            _ticketTypesManager.ChangeTicketTypePrice("Партер", price);
            Assert.AreEqual(_ticketTypesManager.GetTicketTypePrice("Партер"), price);
            _ticketTypesManager.ChangeTicketTypePrice("Партер", oldPrice);
        }
    }
}
