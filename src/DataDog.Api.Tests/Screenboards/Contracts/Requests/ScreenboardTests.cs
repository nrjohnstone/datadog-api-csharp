using System;
using DataDog.Api.Screenboards.Contracts.Requests;
using FluentAssertions;
using NSubstitute.Exceptions;
using Xunit;
using Xunit.Sdk;

namespace DataDog.Api.Tests.Screenboards.Contracts.Requests
{
    public class ScreenboardTests
    {
        private Screenboard CreateSut()
        {
            return new Screenboard();
        }

        [Fact]
        public void Ctor_should_set_template_variables_to_emptyString()
        {
            var sut = CreateSut();
            sut.template_variables.Should().Be(string.Empty);
        }

        [Fact]
        public void Ctor_should_set_description_to_emptyString()
        {
            var sut = CreateSut();
            sut.description.Should().Be(string.Empty);
        }

        [Fact]
        public void Description_when_set_to_null_should_throw()
        {
            var sut = CreateSut();
            Action setDescriptionToNull = () => sut.description = null;

            // assert
            setDescriptionToNull.ShouldThrow<InvalidOperationException>();
        }
    }
}