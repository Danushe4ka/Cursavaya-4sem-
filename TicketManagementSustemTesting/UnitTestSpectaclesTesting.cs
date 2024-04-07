using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TicketManagementSustemTesting
{
    [TestClass]
    public class UnitTestSpectaclesTesting
    {
        SpectaclesManager spectaclesManager = new SpectaclesManager();
        Spectacle spectacle = new Spectacle(1, "test", "test", "test", new DateTime(2000-01-01));
        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(spectaclesManager.Read()[0].SpectacleId, 1);
        }
        [TestMethod]
        public void TestCreate()
        {
            spectaclesManager.Create(spectacle);
            var spectacles = spectaclesManager.Read();
            Assert.AreEqual(spectacles[spectacles.Count - 1].SpectacleName, "test");
            spectaclesManager.Delete(new Spectacle(spectacles[spectacles.Count - 1].SpectacleId, "test1", "test", "test", new DateTime(2000-01-01)));
        }
        [TestMethod]
        public void TestUpdate()
        {
            spectaclesManager.Create(spectacle);
            var spectacles = spectaclesManager.Read();
            int spectacleId = spectacles[spectacles.Count - 1].SpectacleId;
            spectaclesManager.Update(new Spectacle(spectacleId, "test1", "test", "test", new DateTime(2000 - 01 - 01)));
            Assert.IsTrue(spectaclesManager.Read()[spectacles.Count - 1].SpectacleName == "test1");
            spectaclesManager.Delete(new Spectacle(spectacles[spectacles.Count - 1].SpectacleId, "test1", "test", "test", new DateTime(2000 - 01 - 01)));
        }
        [TestMethod]
        public void TestDelete()
        {
            spectaclesManager.Create(spectacle);
            var spectacles = spectaclesManager.Read();
            int spectacleId = spectacles[spectacles.Count - 1].SpectacleId;
            spectaclesManager.Delete(new Spectacle(spectacleId, "test1", "test", "test", new DateTime(2000 - 01 - 01)));
            Assert.IsTrue(spectaclesManager.Read().Count == spectacles.Count - 1);
        }
    }
}
