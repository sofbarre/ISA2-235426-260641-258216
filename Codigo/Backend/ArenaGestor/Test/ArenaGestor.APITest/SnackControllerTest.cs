using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Snack;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class SnackControllerTest
    {
        private Mock<ISnackService> mock;

        private SnacksController api;

        private Snack snackOK;
        private IEnumerable<Snack> snacksOK;
        private Snack insertSnackOK;
        private SnackGetDto resultSnackDto;
        private IEnumerable<SnackGetDto> resultSnacksDto;
        private SnackPostDto insertSnackDto;
        private SnackPutDto updateSnackDto;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ISnackService>(MockBehavior.Strict);

            api = new SnacksController( mock.Object);

            snackOK = new Snack()
            {
                SnackId=1,
                Name = "Papas",
                Description = "Papas fritas Lays",
                Price = 120,
            };

            insertSnackOK = new Snack()
            {
                Name = "Papas",
                Description = "Papas fritas Lays",
                Price = 120,
            };

            snacksOK = new List<Snack>() { snackOK };

            resultSnackDto = new SnackGetDto()
             {
                 SnackId = 1,
                 Name = "Papas",
                 Description = "Papas fritas Lays",
                 Price = 120,
             };

            insertSnackDto= new SnackPostDto()
            {
                Name = "Papas",
                Description = "Papas fritas Lays",
                Price = 120,
            };

            updateSnackDto= new SnackPutDto()
            {
                SnackId= 1,
                Name = "Papas fritas",
                Description = "Papas fritas Lays",
                Price = 120,
            };
        }

        [TestMethod]
        public void GetSnackByIdOkTest()
        {
            mock.Setup(x => x.GetSnackById(snackOK.SnackId)).Returns(snackOK);

            var result = api.GetSnackById(snackOK.SnackId);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetSnacksOkTest()
        {
            mock.Setup(x => x.GetSnacks()).Returns(snacksOK);

            var result = api.GetSnacks();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PostSnackOkTest()
        {
            mock.Setup(x => x.InsertSnack(It.IsAny<Snack>()));

            var result = api.PostSnack(insertSnackDto);
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PutSnackOkTest()
        {
            mock.Setup(x => x.UpdateSnack(It.IsAny<Snack>()));

            var result = api.PutSnack(snackOK.SnackId,updateSnackDto);
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void DeleteSnackOkTest()
        {
            mock.Setup(x => x.DeleteSnack(It.IsAny<int>()));
            var result = api.DeleteSnack(It.IsAny<int>());
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}
