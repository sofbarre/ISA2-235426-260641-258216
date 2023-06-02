using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using ArenaGestor.Domain;
using ArenaGestor.APIContracts.Ticket;
using ArenaGestor.APIContracts.Security;

namespace ArenaGestor.ComprarSnacks.StepDefinitions
{
    [Binding]
    public class CompraSnacksStepDefinitions
    {
        private readonly ScenarioContext context;
        private HttpMethod httpMethod;
        private TicketBuyTicketDto ticket;
        private string routeParams;
        private int contadorSnacks;
        public CompraSnacksStepDefinitions(ScenarioContext context)
        {
            this.context = context;
            this.ticket = new TicketBuyTicketDto();
            this.routeParams = "";
            this.contadorSnacks = 0;
            this.ticket.snackIds = new int[1];

        }
        [Given(@"a ""(.*)"" request of ticket")]
        public void GivenARequestOfTicket(string request)
        {
            switch (request)
            {
                case "get":
                    httpMethod = HttpMethod.Get;
                    break;
                case "post":
                    httpMethod = HttpMethod.Post;
                    break;
                case "put":
                    httpMethod = HttpMethod.Put;
                    break;
                case "delete":
                    httpMethod = HttpMethod.Delete;
                    break;
            }
        }


        [Given(@"a ticketAmount (.*)")]
        public void GivenATicketAmount(int amount)
        {
            ticket.Amount = amount;
        }

        [Given(@"a ConcertId (.*)")]
        public void GivenAConcertId(int id)
        {
            ticket.ConcertId = id;
        }

        [Given(@"a snackId (.*)")]
        public void GivenASnackId(int id)
        {
            ticket.snackIds[contadorSnacks] = id;
            contadorSnacks++;
        }

        [When(@"the request of ""(.*)"" is sent")]
        public async Task WhenTheRequestIsSent(string url)
        {
            // Log in
            var loginRequest = new SecurityLoginDto
            {
                Email = "espectador@example.com",
                Password = "test"
            };
            var loginRequestBody = JsonConvert.SerializeObject(loginRequest);
            var loginRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/Security/login")
            {
                Content = new StringContent(loginRequestBody)
                {
                    Headers =
            {
                ContentType = new MediaTypeHeaderValue("application/json")
            }
                }
            };

            var loginClient = new HttpClient();
            var loginResponse = await loginClient.SendAsync(loginRequestMessage).ConfigureAwait(false);
            // Extract the token from the login response
            var loginResponseContent = await loginResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var securityTokenResponse = JsonConvert.DeserializeObject<SecurityTokenResponseDto>(loginResponseContent);
            this.ticket.snackIds[0] = 2;
            var token = securityTokenResponse.Token;

            var shoppingRequestBody = JsonConvert.SerializeObject(ticket);

            var shoppingRequest = new HttpRequestMessage(this.httpMethod, $"http://localhost:5000/{url}/Shopping")
            {
                Content = new StringContent(shoppingRequestBody)
                {
                    Headers =
            {
                ContentType = new MediaTypeHeaderValue("application/json")
            }
                }
            };

            shoppingRequest.Headers.Add("token", token);

            var shoppingClient = new HttpClient();
            var shoppingResponse = await shoppingClient.SendAsync(shoppingRequest).ConfigureAwait(false);

            try
            {
                context.Set(shoppingResponse.StatusCode, "ResponseStatusCode");
            }
            finally
            {
                shoppingRequest.Dispose();
                shoppingClient.Dispose();
            }
        }




        [Then("the result should be the code (.*)")]
        public void ThenTheResultShouldBe(int StatusCode)
        {
            Assert.Equal(StatusCode, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));

        }
    }
}