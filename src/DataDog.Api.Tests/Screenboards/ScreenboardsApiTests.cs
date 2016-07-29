using System.Linq;
using DataDog.Api.Screenboards;
using DataDog.Api.Screenboards.Contracts;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using RestSharp;
using Xunit;

namespace DataDog.Api.Tests.Screenboards
{
    public class ScreenboardsApiTests
    {
        private readonly IFixture _fixture;
        private readonly Mocks _mocks;

        private class Mocks
        {
            public IRestClient Client { get; set; }
        }

        public ScreenboardsApiTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _mocks = _fixture.Create<Mocks>();
        }

        const string FAKE_DATADOG_HOST = "http://localhost";
        const string FAKE_API_KEY = "FakeApiKey";
        const string FAKE_APP_KEY = "FakeAppKey";

        private ScreenboardsApi CreateSutWithoutMocks()
        {
            return new ScreenboardsApi(new DatadogApiConfig()
            {
                DataDogHost = FAKE_DATADOG_HOST,
                ApiKey = FAKE_API_KEY,
                AppKey = FAKE_APP_KEY
            });
        }

        private ScreenboardsApi CreateSut()
        {
            var sut = CreateSutWithoutMocks();
            sut.Client = _mocks.Client;
            return sut;
        }

        [Fact]
        public void Ctor_should_create_client_with_correct_datadog_host()
        {
            var sut = CreateSutWithoutMocks();
            sut.Client.BaseUrl.Should().Be(FAKE_DATADOG_HOST);
        }

        [Fact]
        public void GetAllScreenboards_should_add_datadog_authentication_keys_to_request()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<ScreenboardSummaries>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.GetAllScreenboards();

            // assert
            AssertRequest_Contains_DataDog_AuthenticationKeys(actualRestRequest);
        }

        private static void AssertRequest_Contains_DataDog_AuthenticationKeys(RestRequest actualRestRequest)
        {
            actualRestRequest.Parameters.Should().Contain(x => x.Name.Equals("api_key"));
            actualRestRequest.Parameters.Should().Contain(x => x.Name.Equals("application_key"));
        }

        [Fact]
        public void GetAllScreenboards_datadog_authentication_keys_should_use_correct_values()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<ScreenboardSummaries>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.GetAllScreenboards();

            // assert
            AssertDataDog_AuthenticationKeyParameters_HaveCorrectValues(actualRestRequest);
        }

        private static void AssertDataDog_AuthenticationKeyParameters_HaveCorrectValues(RestRequest actualRestRequest)
        {
            actualRestRequest.Parameters.First(x => x.Name.Equals("api_key")).Value.Should().Be(FAKE_API_KEY);
            actualRestRequest.Parameters.First(x => x.Name.Equals("application_key")).Value.Should().Be(FAKE_APP_KEY);
        }

        [Fact]
        public void GetScreenboard_should_add_datadog_authentication_keys_to_request()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<Screenboard>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.GetScreenboard(id: _fixture.Create<int>());

            // assert
            AssertRequest_Contains_DataDog_AuthenticationKeys(actualRestRequest);
        }

        [Fact]
        public void GetScreenboard_datadog_authentication_keys_should_use_correct_values()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<Screenboard>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.GetScreenboard(id: _fixture.Create<int>());

            AssertDataDog_AuthenticationKeyParameters_HaveCorrectValues(actualRestRequest);
        }
        
        [Fact]
        public void DeleteScreenboard_should_add_datadog_authentication_keys_to_request()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.DeleteScreenboard(id: _fixture.Create<int>());

            // assert
            AssertRequest_Contains_DataDog_AuthenticationKeys(actualRestRequest);
        }

        [Fact]
        public void DeleteScreenboard_datadog_authentication_keys_should_use_correct_values()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.DeleteScreenboard(id: _fixture.Create<int>());

            AssertDataDog_AuthenticationKeyParameters_HaveCorrectValues(actualRestRequest);
        }

        [Fact]
        public void CreateScreenboard_should_add_datadog_authentication_keys_to_request()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<Screenboard>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.CreateScreenboard(new { board_title = "Test"});

            // assert
            AssertRequest_Contains_DataDog_AuthenticationKeys(actualRestRequest);
        }

        [Fact]
        public void CreateScreenboard_datadog_authentication_keys_should_use_correct_values()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<Screenboard>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.CreateScreenboard(new { board_title = "Test" });

            // assert
            AssertDataDog_AuthenticationKeyParameters_HaveCorrectValues(actualRestRequest);
        }

        [Fact]
        public void CreateScreenboard_datadog_authentication_keys_should_add_json_contentType_header()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<Screenboard>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.CreateScreenboard(new { board_title = "Test" });

            // assert
            actualRestRequest.Parameters.First(x => x.Name.Equals("Content-Type")).Value.Should().Be("application/json");
        }
    }
}