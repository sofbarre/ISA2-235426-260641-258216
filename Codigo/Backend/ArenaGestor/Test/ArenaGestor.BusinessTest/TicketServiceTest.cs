﻿using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class TicketServiceTest
    {
        private Mock<ITicketManagement> managementMock;
        private Mock<IConcertsService> concertServiceMock;
        private Mock<ITicketStatusManagement> ticketStatusManagementMock;
        private TicketService ticketService;
        private Mock<ISnackService> snackServiceMock;

        

        private Mock<ISecurityService> securityServiceMock;

        private TicketStatus buyedStatus;
        private TicketStatus usedStatus;
        private Concert nullConcert;
        private Concert concertOk;
        private Concert concertFullTickets;
        private Ticket nullTicket;
        private Ticket ticketScannedOk;
        private Ticket ticketBuyedOk;
        private List<Ticket> ticketsOK;
        private User userOk;
        private User userNull;

        private TicketSell ticketSellInvalidEmail;
        private TicketSell ticketSellInvalidConcert;
        private TicketSell ticketSellNoMoreTickets;
        private TicketSell ticketSellOK;
        private TicketSell ticketSellNull;
        private TicketBuy ticketBuyInvalidUser;
        private TicketBuy ticketBuyInvalidConcert;
        private TicketBuy ticketBuyNoMoreTickets;
        private TicketBuy ticketBuyOK;
        private TicketBuy ticketBuyNull;

        private Snack snackOk;


        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<ITicketManagement>(MockBehavior.Strict);
            concertServiceMock = new Mock<IConcertsService>(MockBehavior.Strict);
            ticketStatusManagementMock = new Mock<ITicketStatusManagement>(MockBehavior.Strict);
            snackServiceMock = new Mock<ISnackService>(MockBehavior.Strict);


            securityServiceMock = new Mock<ISecurityService>(MockBehavior.Strict);

            ticketService = new TicketService(concertServiceMock.Object, managementMock.Object, ticketStatusManagementMock.Object, securityServiceMock.Object, snackServiceMock.Object);

            nullConcert = null;

            buyedStatus = new TicketStatus()
            {
                TicketStatusId = TicketCode.Comprado,
                Status = TicketCode.Comprado.ToString()
            };

            usedStatus = new TicketStatus()
            {
                TicketStatusId = TicketCode.Utilizado,
                Status = TicketCode.Utilizado.ToString()
            };

            concertOk = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Location = new Location()
                {
                    Country = new Country()
                    {
                        CountryId = 1,
                        Name = "Uruguay"
                    },
                    LocationId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            ticketBuyedOk = new Ticket()
            {
                TicketId = Guid.NewGuid(),
                TicketStatusId = buyedStatus.TicketStatusId,
                Concert = concertOk,
                TicketStatus = buyedStatus,
                Email = "test@user.com",
                ConcertId = concertOk.ConcertId,
                Amount = 1
            };

            snackOk = new Snack()
            {
                SnackId = 0,
                Name = "snack ok",
                Description = "papas fritas",
                Price = 20

            };

            concertOk.Tickets = new List<Ticket>()
            {
                ticketBuyedOk
            };

            ticketScannedOk = new Ticket()
            {
                TicketId = Guid.NewGuid(),
                TicketStatusId = usedStatus.TicketStatusId,
                Concert = concertOk,
                TicketStatus = usedStatus,
                Email = "test@user.com",
                ConcertId = concertOk.ConcertId,
                Amount = 1
            };

            concertFullTickets = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 1,
                Tickets = new List<Ticket>()
                {
                    ticketBuyedOk
                },
                Location = new Location()
                {
                    Country = new Country()
                    {
                        CountryId = 1,
                        Name = "Uruguay"
                    },
                    LocationId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            ticketsOK = new List<Ticket>
            {
                ticketBuyedOk
            };

            userOk = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        RoleId = RoleCode.Administrador
                    }
                }
            };
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TicketSellNull()
        {
            ticketSellNull = null;

            Ticket ticket = ticketService.SellTicket(ticketSellNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void SellTicketInvalidAmountTest()
        {
            ticketSellOK = new TicketSell()
            {
                Amount = 0,
                ConcertId = concertOk.ConcertId,
                Email = "test@test.com"
            };

            Ticket ticket = ticketService.SellTicket(ticketSellOK);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void SellTicketInvalidAmount2Test()
        {
            ticketSellOK = new TicketSell()
            {
                Amount = -1,
                ConcertId = concertOk.ConcertId,
                Email = "test@test.com"
            };

            Ticket ticket = ticketService.SellTicket(ticketSellOK);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InvalidEmailTest()
        {
            ticketSellInvalidEmail = new TicketSell()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                Email = "xxxx"
            };

            Ticket ticket = ticketService.SellTicket(ticketSellInvalidEmail);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InvalidConcertTest()
        {
            ticketSellInvalidEmail = new TicketSell()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                Email = "test@test.com"
            };

            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(nullConcert);
            Ticket ticket = ticketService.SellTicket(ticketSellInvalidEmail);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void NoMoreTicketsTest()
        {
            ticketSellNoMoreTickets = new TicketSell()
            {
                Amount = 1,
                ConcertId = concertFullTickets.ConcertId,
                Email = "test@test.com"
            };

            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertFullTickets);
            Ticket ticket = ticketService.SellTicket(ticketSellNoMoreTickets);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ExceedsLimitTest()
        {
            ticketSellNoMoreTickets = new TicketSell()
            {
                Amount = 500,
                ConcertId = concertOk.ConcertId,
                Email = "test@test.com"
            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            Ticket ticket = ticketService.SellTicket(ticketSellNoMoreTickets);
        }

        [TestMethod]
        public void TicketNullTicketsTest()
        {
            ticketSellOK = new TicketSell()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                Email = "test@test.com"
            };

            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(buyedStatus);
            concertOk.Tickets = null;
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.InsertTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            Ticket ticket = ticketService.SellTicket(ticketSellOK);
            concertServiceMock.VerifyAll();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void TicketOkTest()
        {
            ticketSellOK = new TicketSell()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                Email = "test@test.com"
            };

            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(buyedStatus);
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.InsertTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            Ticket ticket = ticketService.SellTicket(ticketSellOK);
            concertServiceMock.VerifyAll();
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void ScanTicketNotExistsTest()
        {
            managementMock.Setup(x => x.GetTicketById(It.IsAny<Guid>())).Returns(nullTicket);
            ticketService.ScanTicket(Guid.NewGuid());
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ScanTicketAlreadyScannedTest()
        {
            managementMock.Setup(x => x.GetTicketById(It.IsAny<Guid>())).Returns(ticketScannedOk);
            ticketService.ScanTicket(Guid.NewGuid());
        }

        [TestMethod]
        public void ScanTicketOkTest()
        {
            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(usedStatus);
            managementMock.Setup(x => x.GetTicketById(It.IsAny<Guid>())).Returns(ticketBuyedOk);
            managementMock.Setup(x => x.UpdateTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            Ticket ticket = ticketService.ScanTicket(Guid.NewGuid());
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetTicketsByUserNoTicketsTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);
            managementMock.Setup(x => x.GetTicketsByUser(It.IsAny<string>())).Returns(new List<Ticket>());
            var tickets = ticketService.GetTicketsByUser("notexists@gmail.com");
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetTicketsByUserTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);
            managementMock.Setup(x => x.GetTicketsByUser(It.IsAny<string>())).Returns(ticketsOK);
            var tickets = ticketService.GetTicketsByUser(ticketBuyedOk.Email);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void BuyTicketInvalidUserTest()
        {
            ticketBuyInvalidUser = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[0]

            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userNull);
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);

            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyInvalidUser);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void BuyTicketInvalidConcertTest()
        {
            ticketBuyInvalidConcert = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[0]

            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(nullConcert);
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyInvalidConcert);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BuyTicketNoMoreTicketsTest()
        {
            ticketBuyNoMoreTickets = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertFullTickets.ConcertId,
                snackIds = new int[0]

            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertFullTickets);
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyNoMoreTickets);
        }

        [TestMethod]
        public void BuyTicketNullTicketsTest()
        {
            ticketBuyOK = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[0]

            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(buyedStatus);
            concertOk.Tickets = null;
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.InsertTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyOK);
            concertServiceMock.VerifyAll();
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BuyTicketExceedsLimitTest()
        {
            ticketBuyNoMoreTickets = new TicketBuy()
            {
                Amount = 500,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[0]

            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyNoMoreTickets);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BuyTicketAmountIncorrect()
        {
            ticketBuyOK = new TicketBuy()
            {
                Amount = 0,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[0]
            };


            Ticket ticket = ticketService.BuyTicket("", ticketBuyOK);
        }

        [TestMethod]
        public void BuyTicketOkTest()
        {
            ticketBuyOK = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[0]
            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(buyedStatus);
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.InsertTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyOK);
            concertServiceMock.VerifyAll();
            managementMock.VerifyAll();
        }


        [TestMethod]
        public void BuyTicketWithSnacksOkTest()
        {
            ticketBuyOK = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[2]
            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(buyedStatus);
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.InsertTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            managementMock.Setup(x => x.InsertTicketSnack(It.IsAny<TicketSnack>()));
            snackServiceMock.Setup(x => x.GetSnackById(It.IsAny<int>())).Returns(snackOk);
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyOK);
            concertServiceMock.VerifyAll();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void BuyTicketWith100SnacksOkTest()
        {
            ticketBuyOK = new TicketBuy()
            {
                Amount = 1,
                ConcertId = concertOk.ConcertId,
                snackIds = new int[100]
            };

            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);

            ticketStatusManagementMock.Setup(x => x.GetStatus(It.IsAny<TicketCode>())).Returns(buyedStatus);
            concertServiceMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.InsertTicket(It.IsAny<Ticket>()));
            managementMock.Setup(x => x.Save());
            managementMock.Setup(x => x.InsertTicketSnack(It.IsAny<TicketSnack>()));
            snackServiceMock.Setup(x => x.GetSnackById(It.IsAny<int>())).Returns(snackOk);
            Ticket ticket = ticketService.BuyTicket(It.IsAny<string>(), ticketBuyOK);
            concertServiceMock.VerifyAll();
            managementMock.VerifyAll();
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TicketBuyNull()
        {
            ticketBuyNull = null;

            Ticket ticket = ticketService.BuyTicket("", ticketBuyNull);
        }

    }
}
