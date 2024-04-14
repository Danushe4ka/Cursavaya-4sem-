using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestTicketTypesTesting
    {
        TicketTypesManager ticketTypesManager = new TicketTypesManager();
        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(ticketTypesManager.Read()[0].TypeId, 1);
        }
        [TestMethod]
        public void TestUpdate()
        {
            TicketType ticketType = new TicketType(1, "test", 25, 513.199);
            ticketTypesManager.Update(ticketType);
            Assert.AreEqual(ticketTypesManager.Read()[1].TypeCost, ticketType.TypeCost);
        }
    }
}
