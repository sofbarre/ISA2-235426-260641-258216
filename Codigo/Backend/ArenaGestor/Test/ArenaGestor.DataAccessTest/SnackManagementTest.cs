using ArenaGestor.DataAccess.Managements;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.DataAccessTest
{
    [TestClass]
    public class Snackmanagement : ManagementTest
    {

        private DbContext context;
        private SnackManagement management;

        private Snack snackOK;
        private Snack snackNotExists;
        private List<Snack> snacksOK;
        private List<Snack> snacksAdded;

        [TestInitialize]
        public void InitTest()
        {
            snackOK = new Snack()
            {
                SnackId = 1,
                Name="Papas",
                Description="Papas fritas Lays",
                Price=120,
            };

            snackNotExists= new Snack()
            {
                SnackId = 2,
                Name = "Mani",
                Description = "Mani salado",
                Price = 220,
            };

            snacksOK = new List<Snack>
            {
                snackOK
            };

            snacksAdded = new List<Snack>
            {
                snackOK,
                snackNotExists
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Snack>().Add(snackOK);
            context.SaveChanges();

            management = new SnackManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetSnacks().ToList();
            Assert.IsTrue(snacksOK.SequenceEqual(result));
        }


        [TestMethod]
        public void GetById()
        {
            Snack snack = management.GetSnackById(snackOK.SnackId);
            Assert.AreEqual(snackOK, snack);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertSnack(snackNotExists);
            var result = management.GetSnacks().ToList();
            Assert.IsTrue(snacksAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            snackOK.Name = "Papitas";
            management.UpdateSnack(snackOK);
            string newName = management.GetSnacks().Where(g => g.SnackId == snackOK.SnackId).First().Name;
            Assert.AreEqual("Papitas", newName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteSnack(snackOK.SnackId);
            int size = management.GetSnacks().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
