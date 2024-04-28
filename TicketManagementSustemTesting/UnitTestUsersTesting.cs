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
        UsersManager _usersManager = new UsersManager();
        BaseUser _newUser = new RegUser("test", "test");
        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(_usersManager.Read()[0].Role,"Admin");
        }
        [TestMethod]
        public void TestCreate()
        {
            _usersManager.Create(_newUser);
            var users = _usersManager.Read();
            Assert.AreEqual(users[users.Count-1].UserName, "test");
            _usersManager.Delete(_newUser);
        }
        [TestMethod]
        public void TestUpdate()
        {
            _usersManager.Create(_newUser);
            var users = _usersManager.Read();
            _usersManager.Update(new RegUser("test", "test1"));
            Assert.IsTrue(_usersManager.Read()[users.Count-1].Password == "test1");
            _usersManager.Delete(new RegUser("test", "test1"));
        }
        [TestMethod]
        public void TestDelete()
        {
            _usersManager.Create(_newUser);
            var users = _usersManager.Read();
            _usersManager.Delete(_newUser);
            Assert.IsTrue(_usersManager.Read().Count == users.Count-1 );
        }
    }
}
