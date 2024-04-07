using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBObjectsClassLibrary;
using System;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestUsersTesting
    {
        UsersManager usersManager = new UsersManager();
        BaseUser user = new RegUser(1, "test", "test");
        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(usersManager.Read()[0].Role,"Admin");
        }
        [TestMethod]
        public void TestCreate()
        {
            usersManager.Create(user);
            var users = usersManager.Read();
            Assert.AreEqual(users[users.Count-1].UserName, "test");
            usersManager.Delete(new RegUser(users[users.Count - 1].UserId, "test1", "test"));
        }
        [TestMethod]
        public void TestUpdate()
        {
            usersManager.Create(user);
            var users = usersManager.Read();
            int userId = users[users.Count-1].UserId;
            usersManager.Update(new RegUser(userId, "test1", "test"));
            Assert.IsTrue(usersManager.Read()[users.Count-1].UserName == "test1");
            usersManager.Delete(new RegUser(users[users.Count - 1].UserId, "test1", "test"));
        }
        [TestMethod]
        public void TestDelete()
        {
            usersManager.Create(user);
            var users = usersManager.Read();
            int userId = users[users.Count - 1].UserId;
            usersManager.Delete(new RegUser(userId, "test1", "test"));
            Assert.IsTrue(usersManager.Read().Count == users.Count-1 );
        }
    }
}
