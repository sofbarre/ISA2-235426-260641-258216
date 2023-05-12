using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using ArenaGestor.Domain;
using ArenaGestor.APIContracts.Ticket;

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
            string requestBody = JsonConvert.SerializeObject(ticket);

            var request = new HttpRequestMessage(this.httpMethod, $"http://localhost:44372/{url}/Shopping")
            {
                Content = new StringContent(requestBody)
                {
                    Headers =
                        {
                          ContentType = new MediaTypeHeaderValue("application/json")
                        }
                }
            };
            // create an http client
            var client = new HttpClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);
            try
            {
                context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
                // move along, move along
            }
        }

        [Then("the result should be the code (.*)")]
        public void ThenTheResultShouldBe(int StatusCode)
        {
            Assert.Equal(StatusCode, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));

        }
    }
}