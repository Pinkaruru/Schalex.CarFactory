using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class InteriorProviderTests
    {
        private readonly IInteriorProvider _interiorProvider;

        public InteriorProviderTests()
        {
            _interiorProvider = new InteriorProvider(new DashboardBuilder(), new SeatBuilder(), new SpeakerBuilder());
        }

        [TestMethod]
        public void GetInterior_InteriorShouldNotBeNull()
        {
            // Arrange
            var paintJob = new StripedPaintJob(Color.FromName("red"), Color.FromName("white"));
            var doorSpeakers = new List<CarSpecification.SpeakerSpecification>
                                        {
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true }
                                        };

            var dashboardSpeakers = new List<CarSpecification.SpeakerSpecification>()
                                        {
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true },
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true }
                                        };

            var spec = new CarSpecification(paintJob, Manufacturer.Plandrover, 5, doorSpeakers, dashboardSpeakers);
            
            // Act
            var interior = _interiorProvider.GetInterior(spec);

            // Assert
            interior.Should().NotBeNull();
        }

        [TestMethod]
        public void GetInterior_InteriorDoorSpeakersAmountShouldBeEqual_AsGivenSpeakers()
        {
            // Arrange
            var paintJob = new StripedPaintJob(Color.FromName("red"), Color.FromName("white"));
            var doorSpeakers = new List<CarSpecification.SpeakerSpecification>
                                        {
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true }
                                        };

            var dashboardSpeakers = new List<CarSpecification.SpeakerSpecification>()
                                        {
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true },
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true }
                                        };

            var spec = new CarSpecification(paintJob, Manufacturer.Plandrover, 5, doorSpeakers, dashboardSpeakers);

            // Act
            var interior = _interiorProvider.GetInterior(spec);

            // Assert
            interior.DoorSpeakers.Count().Should().Be(doorSpeakers.Count);
        }

        [TestMethod]
        public void GetInterior_InteriorDashboardSpeakersAmountShouldBeEqual_AsGivenSpeakers()
        {
            // Arrange
            var paintJob = new StripedPaintJob(Color.FromName("red"), Color.FromName("white"));
            var doorSpeakers = new List<CarSpecification.SpeakerSpecification>
                                        {
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true }
                                        };

            var dashboardSpeakers = new List<CarSpecification.SpeakerSpecification>()
                                        {
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true },
                                           new CarSpecification.SpeakerSpecification { IsSubwoofer = true }
                                        };

            var spec = new CarSpecification(paintJob, Manufacturer.Plandrover, 5, doorSpeakers, dashboardSpeakers);

            // Act
            var interior = _interiorProvider.GetInterior(spec);

            // Assert
            interior.FrontWindowSpeakers.Count().Should().Be(dashboardSpeakers.Count);
        }
    }
}
