﻿using System.Linq;
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
            actualRestRequest.Parameters.Should().Contain(x => x.Name.Equals("api_key"));
            actualRestRequest.Parameters.Should().Contain(x => x.Name.Equals("application_key"));
        }

        [Fact]
        public void GetAllScreenboards_should_use_correct_apiKey_value()
        {
            var sut = CreateSut();

            RestRequest actualRestRequest = new RestRequest();
            _mocks.Client.Execute<ScreenboardSummaries>(Arg.Do<RestRequest>(x => actualRestRequest = x));

            // act
            sut.GetAllScreenboards();

            // assert
            actualRestRequest.Parameters.First(x => x.Name.Equals("api_key")).Value.Should().Be(FAKE_API_KEY);
            actualRestRequest.Parameters.First(x => x.Name.Equals("application_key")).Value.Should().Be(FAKE_APP_KEY);
        }

    }
}