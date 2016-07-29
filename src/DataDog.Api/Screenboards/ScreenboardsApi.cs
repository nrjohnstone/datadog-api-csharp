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

            request.AddQueryParameter("api_key", _datadogApiConfig.ApiKey);
            request.AddQueryParameter("application_key", _datadogApiConfig.AppKey);

            return Client.Execute<ScreenboardSummaries>(request);
        }

        public IRestResponse<Screenboard> GetScreenboard(int id)
        {
            var request = new RestRequest($"{SCREEN_ENDPOINT}/{id}", Method.GET);

            request.AddQueryParameter("api_key", _datadogApiConfig.ApiKey);
            request.AddQueryParameter("application_key", _datadogApiConfig.AppKey);

            return Client.Execute<Screenboard>(request);
        }

        public IRestResponse DeleteScreenboard(int id)
        {
            var request = new RestRequest($"{SCREEN_ENDPOINT}/{id}", Method.DELETE);

            request.AddQueryParameter("api_key", _datadogApiConfig.ApiKey);
            request.AddQueryParameter("application_key", _datadogApiConfig.AppKey);

            return Client.Execute(request);
        }
    }
}