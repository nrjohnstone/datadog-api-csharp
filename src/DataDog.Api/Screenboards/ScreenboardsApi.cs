using DataDog.Api.Screenboards.Contracts;
using RestSharp;

namespace DataDog.Api.Screenboards
{
    public class ScreenboardsApi
    {
        private readonly DatadogApiConfig _datadogApiConfig;

        const string SCREEN_ENDPOINT = "/api/v1/screen";

        public ScreenboardsApi(DatadogApiConfig datadogApiConfig)
        {
            _datadogApiConfig = datadogApiConfig;
            Client = new RestClient(_datadogApiConfig.DataDogHost);
        }

        internal IRestClient Client { get; set; }

        public IRestResponse<ScreenboardSummaries> GetAllScreenboards()
        {
            var request = new RestRequest(SCREEN_ENDPOINT, Method.GET);

            AddAuthenticationParameters(request);

            return Client.Execute<ScreenboardSummaries>(request);
        }

        private void AddAuthenticationParameters(RestRequest request)
        {
            request.AddQueryParameter("api_key", _datadogApiConfig.ApiKey);
            request.AddQueryParameter("application_key", _datadogApiConfig.AppKey);
        }

        public IRestResponse<Screenboard> GetScreenboard(int id)
        {
            var request = new RestRequest($"{SCREEN_ENDPOINT}/{id}", Method.GET);

            AddAuthenticationParameters(request);

            return Client.Execute<Screenboard>(request);
        }

        public IRestResponse DeleteScreenboard(int id)
        {
            var request = new RestRequest($"{SCREEN_ENDPOINT}/{id}", Method.DELETE);

            AddAuthenticationParameters(request);

            return Client.Execute(request);
        }

        public IRestResponse<Screenboard> CreateScreenboard(object screenboardRequest)
        {
            var request = new RestRequest(SCREEN_ENDPOINT, Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("api_key", _datadogApiConfig.ApiKey);
            request.AddQueryParameter("application_key", _datadogApiConfig.AppKey);

            request.AddJsonBody(screenboardRequest);
           
            return Client.Execute<Screenboard>(request);
        }
    }
}