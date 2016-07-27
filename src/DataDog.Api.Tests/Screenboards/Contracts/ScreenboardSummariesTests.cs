using System;
using System.IO;
using DataDog.Api.Screenboards.Contracts;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using RestSharp;
using RestSharp.Deserializers;
using Xunit;

namespace DataDog.Api.Tests.Screenboards.Contracts
{
    public class ScreenboardSummariesTests
    {
        [Fact]
        public void Can_deserialize_json_screenboards_response()
        {
            string json = File.ReadAllText("./TestResponses/GetAllScreenboardsResponse.json");
            json.Should().NotBeEmpty();
            var response = new RestResponse() { Content = json };

            var jsonDeserializer = new JsonDeserializer();

            // act
            var x = jsonDeserializer.Deserialize<ScreenboardSummaries>(response);

            // assert
            x.Screenboards.Count.Should().Be(3);

            var screenboard1 = x.Screenboards[0];
            screenboard1.Id.Should().Be(123456);
            screenboard1.Title.Should().Be("Team 1 ReadOnly Dashboard");
            screenboard1.Created.Should().BeCloseTo(DateTime.Parse("2016-07-21 11:54:09.948"));
            screenboard1.Modified.Should().BeCloseTo(DateTime.Parse("2016-07-26 14:23:41.136"));
            screenboard1.ReadOnly.Should().BeTrue();
            screenboard1.Resource.Should().Be("/api/v1/screen/123456");

            var screenboard2 = x.Screenboards[1];
            screenboard2.Id.Should().Be(2468);
            screenboard2.Title.Should().Be("Team 2 Dashboard");
            screenboard2.Created.Should().BeCloseTo(DateTime.Parse("2016-06-01 16:29:58.381"));
            screenboard2.Modified.Should().BeCloseTo(DateTime.Parse("2016-07-26 09:18:27.450"));
            screenboard2.ReadOnly.Should().BeFalse();
            screenboard2.Resource.Should().Be("/api/v1/screen/2468");

            var screenboard3 = x.Screenboards[2];
            screenboard3.Id.Should().Be(135);
            screenboard3.Title.Should().Be("Team 3 Dashboard");
            screenboard3.Created.Should().BeCloseTo(DateTime.Parse("2016-05-23 09:29:05.494"));
            screenboard3.Modified.Should().BeCloseTo(DateTime.Parse("2016-07-26 16:48:01.538"));
            screenboard3.ReadOnly.Should().BeFalse();
            screenboard3.Resource.Should().Be("/api/v1/screen/135");
        }
    }
}