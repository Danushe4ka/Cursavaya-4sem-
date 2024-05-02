using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Npgsql;
using DBObjectsClassLibrary.Models.Spectacles;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestSpectaclesTesting
    {
        NpgsqlCommand _cmd = new NpgsqlCommand();
        SpectaclesManager _spectaclesManager = new SpectaclesManager();
        Spectacle _newSpectacle = new DramaSpectacle("Test", "testauthor", new DateTime(2000, 1, 1));
        [TestMethod]
        public void TestRead()
        {
            _cmd.Connection = DBConnectionManager.GetInstance.Connection;
            _cmd.CommandText = "select count(*) from Spectacles";
            int count = Convert.ToInt32(_cmd.ExecuteScalar());
            Assert.AreEqual(_spectaclesManager.Read().Count, count);
        }
        [TestMethod]
        public void TestCreate()
        {
            _spectaclesManager.Create(_newSpectacle);
            var spectacles = _spectaclesManager.Read();
            Assert.AreEqual(spectacles[spectacles.Count - 1].SpectacleName, "Test");
            _spectaclesManager.Delete(_newSpectacle);
        }
        [TestMethod]
        public void TestUpdate()
        {
            _spectaclesManager.Create(_newSpectacle);
            var spectacles = _spectaclesManager.Read();
            _spectaclesManager.Update(new NovelSpectacle("Test1", "testauthor", new DateTime(2000, 1, 1)));
            Assert.IsTrue(_spectaclesManager.Read()[spectacles.Count - 1].SpectacleName == "Test1");
            _spectaclesManager.Delete(new NovelSpectacle("Test1", "testauthor", new DateTime(2000, 1, 1)));
        }
        [TestMethod]
        public void TestDelete()
        {
            _spectaclesManager.Create(_newSpectacle);
            var spectacles = _spectaclesManager.Read();
            _spectaclesManager.Delete(_newSpectacle);
            Assert.IsTrue(_spectaclesManager.Read().Count == spectacles.Count - 1);
        }
    }
}
