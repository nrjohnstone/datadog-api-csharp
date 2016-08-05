using System;
using System.IO;
using DataDog.Api.Screenboards.Contracts.Responses;
using FluentAssertions;
using RestSharp;
using RestSharp.Deserializers;
using Xunit;

namespace DataDog.Api.Tests.Screenboards.Contracts.Responses
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
            screenboard1.Created.Should().BeCloseTo(DateTimeOffset.Parse("2016-07-21 09:54:09.948+00:00"));
            screenboard1.Modified.Should().BeCloseTo(DateTimeOffset.Parse("2016-07-26 12:23:41.13+00:00"));
            screenboard1.ReadOnly.Should().BeTrue();
            screenboard1.Resource.Should().Be("/api/v1/screen/123456");

            var screenboard2 = x.Screenboards[1];
            screenboard2.Id.Should().Be(2468);
            screenboard2.Title.Should().Be("Team 2 Dashboard");
            screenboard2.Created.Should().BeCloseTo(DateTimeOffset.Parse("2016-06-01 14:29:58.381+00:00"));
            screenboard2.Modified.Should().BeCloseTo(DateTimeOffset.Parse("2016-07-26 07:18:27.450+00:00"));
            screenboard2.ReadOnly.Should().BeFalse();
            screenboard2.Resource.Should().Be("/api/v1/screen/2468");
            
            var screenboard3 = x.Screenboards[2];
            screenboard3.Id.Should().Be(135);
            screenboard3.Title.Should().Be("Team 3 Dashboard");
            screenboard3.Created.Should().BeCloseTo(DateTimeOffset.Parse("2016-05-23 07:29:05.494+00:00"));
            screenboard3.Modified.Should().BeCloseTo(DateTimeOffset.Parse("2016-07-26 14:48:01.538+00:00"));
            screenboard3.ReadOnly.Should().BeFalse();
            screenboard3.Resource.Should().Be("/api/v1/screen/135");
        }
    }
}