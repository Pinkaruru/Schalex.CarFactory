using CarFactory_Interior.Builders;
using CarFactory_Interior.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarFactory_Factory.CarSpecification;

namespace UnitTests
{
    [TestClass]
    public class SpeakerBuilderTests
    {
        private readonly ISpeakerBuilder _speakerBuilder;

        public SpeakerBuilderTests()
        {
            _speakerBuilder = new SpeakerBuilder();
        }

        [TestMethod]
        public void BuildDoorSpeakers_SpeakersShouldNotBeNull()
        {
            // Arrange
            var speakerSpecifications = new List<SpeakerSpecification>()
            {
                new SpeakerSpecification
                {
                    IsSubwoofer = false
                }
            };

            // Act
            var doorSpeakers = _speakerBuilder.BuildDoorSpeakers(speakerSpecifications);

            // Assert
            doorSpeakers.Should().NotBeNull();
        }

        [TestMethod]
        public void BuildFrontWindowSpeakers_SpeakersShouldNotBeNull()
        {
            // Arrange
            var speakerSpecifications = new List<SpeakerSpecification>()
            {
                new SpeakerSpecification
                {
                    IsSubwoofer = false
                }
            };

            // Act
            var frontWindowSpeakers = _speakerBuilder.BuildFrontWindowSpeakers(speakerSpecifications);

            // Assert
            frontWindowSpeakers.Should().NotBeNull();
        }

        [TestMethod]
        public void BuildFrontWindowSpeakers_ShouldThrowArgumentException_WhenGivenMoreThanTwoSpeakerSpecs()
        {
            // Arrange
            var speakerSpecifications = new List<SpeakerSpecification>()
            {
                new SpeakerSpecification
                {
                    IsSubwoofer = false
                },
                new SpeakerSpecification
                {
                    IsSubwoofer = false
                },
                new SpeakerSpecification
                {
                    IsSubwoofer = false
                }
            };

            // Act
            Action buildFrontSpeakersAction = () =>
            {
                _speakerBuilder.BuildFrontWindowSpeakers(speakerSpecifications);
            };

            // Assert
            buildFrontSpeakersAction.Should().Throw<ArgumentException>()
                .WithMessage("More than 2 speakers aren't supported");
        }
    }
}
