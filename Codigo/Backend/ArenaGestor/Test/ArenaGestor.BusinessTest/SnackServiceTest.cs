using ArenaGestor.Business;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.BusinessTest
{
    
    [TestClass]
    public class SnackServiceTest
    {
        private Mock<ISnackManagement> managementMock;
        private SnackService snackService;

        private Snack snackOK;
        private Snack snackEmpty;
        private Snack snackNull;
        private int snackIdZero;
        private int snackIdInexistant;
        private List<Snack> snacks;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<ISnackManagement>(MockBehavior.Strict);
            snackService = new SnackService(managementMock.Object);

            snackOK = new Snack()
            {
                SnackId = 1,
                Name = "Papas",
                Description = "Papas fritas Lays",
                Price = 120,
            };
            snackEmpty = new Snack()
            {
                SnackId = 1,
                Name = "",
                Description = "Papas fritas Lays",
                Price = 120,
            };

            snackNull = null;
            snackIdZero = 0;
            snackIdInexistant = 2;
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetSnackByIdInvalidIdTest()
        {
            managementMock.Setup(x => x.GetSnackById(snackIdZero)).Returns(snackNull);
            snackService.GetSnackById(snackIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetSnackByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetSnackById(snackIdInexistant)).Returns(snackNull);
            snackService.GetSnackById(snackIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetSnackByIdOkTest()
        {
            managementMock.Setup(x => x.GetSnackById(snackOK.SnackId)).Returns(snackOK);
            snackService.GetSnackById(snackOK.SnackId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetSnacksOkTest()
        {
            managementMock.Setup(x => x.GetSnacks()).Returns(snacks);
            snackService.GetSnacks();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void InsertSnacksOkTest()
        {
            managementMock.Setup(x => x.InsertSnack(snackOK));
            snackService.InsertSnack(snackOK);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InsertSnackEmptyNameTest()
        {
            snackService.InsertSnack(snackEmpty);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InsertSnackWrongPriceTest()
        {
            snackEmpty.Name = "Mani";
            snackEmpty.Price = -39;
            snackService.InsertSnack(snackEmpty);

        }
        [TestMethod]
        public void UpdateSnackOKTest()
        {
            managementMock.Setup(x => x.GetSnackById(It.IsAny<int>())).Returns(snackOK);
            managementMock.Setup(x => x.UpdateSnack(snackOK));
            snackService.UpdateSnack(snackOK);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateSnackEmptyNameTest()
        {
            snackService.UpdateSnack(snackEmpty);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateSnackWrongPriceTest()
        {
            snackEmpty.Name = "Mani";
            snackEmpty.Price = -39;
            snackService.UpdateSnack(snackEmpty);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateSnackNullTest()
        {
            snackEmpty.Name = "Mani";
            snackEmpty.Price = -39;
            managementMock.Setup(x => x.GetSnackById(It.IsAny<int>())).Returns(snackNull);
            snackService.UpdateSnack(snackEmpty);

        }
        [TestMethod]
        public void DeleteSnackOKTest()
        {
            managementMock.Setup(x => x.GetSnackById(It.IsAny<int>())).Returns(snackOK);
            managementMock.Setup(x => x.DeleteSnack(It.IsAny<int>()));
            snackService.DeleteSnack(snackOK.SnackId);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteSnackNullTest()
        {
            managementMock.Setup(x => x.GetSnackById(It.IsAny<int>())).Returns(snackNull);
            snackService.DeleteSnack(snackOK.SnackId);

        }
    }
    
    
}
