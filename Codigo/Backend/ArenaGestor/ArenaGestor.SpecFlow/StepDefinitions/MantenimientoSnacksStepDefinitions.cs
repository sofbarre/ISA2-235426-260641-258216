using ArenaGestor.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;


namespace ArenaGestor.SpecFlow.StepDefinitions
{
    [Binding]
    public class MantenimientoSnacksStepDefinitions
    {
        private readonly ScenarioContext context;
        private HttpMethod httpMethod;
        private Snack snack;
        private string routeParams;
        public MantenimientoSnacksStepDefinitions(ScenarioContext context)
        {
            this.context = context;
            this.snack = new Snack();
            this.routeParams = "";
        }

        [Given(@"a ""(.*)"" request of snacks")]

        public void GivenARequest(string request)
        {
            switch(request)
            {
                case "get":
                    httpMethod = HttpMethod.Get;
                    break;
                case "post":
                    httpMethod = HttpMethod.Post; 
                    break;
                case "put":
                    httpMethod = HttpMethod.Get;
                    break;
                case "delete":
                    httpMethod = HttpMethod.Delete; 
                    break;
            }
        }

        [Given(@"a snack name ""(.*)""")]
        public void GivenASnackWithName(string name)
        {
            snack.Name = name;
        }
        [Given(@"a snack description ""(.*)""")]
        public void GivenASnackWithDescription(string description)
        {
            snack.Description = description;
        }
        [Given(@"a snack price ""(.*)""")]
        public void GivenASnackWithPrice(int price)
        {
            snack.Price = price;
        }

        [Given(@"a snack with id (.*)")]
        public void GivenASnackWithId(int id)
        {
            routeParams = "/" + id;
        }

        [When(@"the request of ""(.*)"" is sent")]
        public async Task WhenTheRequestIsSent(string url)
        {
            string requestBody = JsonConvert.SerializeObject(snack);

            var request = new HttpRequestMessage(this.httpMethod, $"http://localhost:48227/api/{url + routeParams}")
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