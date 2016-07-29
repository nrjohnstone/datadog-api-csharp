using System;
using System.IO;
using DataDog.Api.Screenboards.Contracts;
using FluentAssertions;
using RestSharp;
using RestSharp.Deserializers;
using Xunit;

namespace DataDog.Api.Tests.Screenboards.Contracts
{
    public class ScreenboardTests
    {
        [Fact]
        public void Can_deserialize_json_screenboards_response()
        {
            string json = File.ReadAllText("./TestResponses/GetScreenboardResponse.json");
            json.Should().NotBeEmpty();

            var jsonDeserializer = new JsonDeserializer();

            // act
            var x = jsonDeserializer.Deserialize<Screenboard>(new RestResponse() {Content = json});

            // assert
            x.BoardTitle.Should().Be("Board title for Team 1");
            x.Height.Should().Be("80");
            x.Width.Should().Be("100%");
            x.ReadOnly.Should().BeTrue();
            x.BoardBgtype.Should().Be("board_graph");
            x.Created.Should().BeCloseTo(DateTimeOffset.Parse("2015-07-21T09:54:09.948191+00:00"));
            x.OriginalTitle.Should().Be("Original board title for Team 1");
            x.Modified.Should().BeCloseTo(DateTimeOffset.Parse("2015-07-26T12:23:41.136208+00:00"));
            x.Templated.Should().BeTrue();
            x.Shared.Should().BeTrue();
            x.Id.Should().Be(1234);
            x.TitleEdited.Should().BeTrue();
        }
    }
}