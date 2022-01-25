using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_Wheels;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class WheelProviderTests
    {
        private readonly IWheelProvider _wheelProvider;
        private readonly Mock<IGetRubberQuery> _getRubberQueryMock = new Mock<IGetRubberQuery>();

        public WheelProviderTests()
        {
            _wheelProvider = new WheelProvider(_getRubberQueryMock.Object);
        }

        [TestMethod]
        public void GetWheels_ShouldBeExactly4Wheels()
        {
            // Arrange
            var parts = new List<Part>
            {
                new Part
                {
                    Manufacturer = Manufacturer.Volksday,
                    PartType = PartType.Rubber
                }
            };

            _getRubberQueryMock
                .Setup(query => query.Get())
                .Returns(parts);

            // Act
            var wheels = _wheelProvider.GetWheels();

            // Assert
            wheels.Count().Should().Be(4);
        }

        [TestMethod]
        public void GetWheels_ShouldHaveSameManufacturerAsRubber()
        {
            // Arrange
            var rubberManufacturer = Manufacturer.Volksday;
            var parts = new List<Part>
            {
                new Part
                {
                    Manufacturer = rubberManufacturer,
                    PartType = PartType.Rubber
                }
            };

            _getRubberQueryMock
                .Setup(query => query.Get())
                .Returns(parts);

            // Act
            var wheels = _wheelProvider.GetWheels();

            // Assert
            wheels.Should().OnlyContain(w => w.Manufacturer == rubberManufacturer);
        }
    }
}
