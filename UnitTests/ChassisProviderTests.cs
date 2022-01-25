using CarFactory_Chasis;
using CarFactory_Domain;
using CarFactory_Domain.Exceptions;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests
{
    [TestClass]
    public class ChassisProviderTests
    {
        private readonly Mock<IGetChassisRecipeQuery> _getChassisRecipeQueryMock = new Mock<IGetChassisRecipeQuery>();
        private readonly Mock<IGetRubberQuery> _getRubberQueryMock = new Mock<IGetRubberQuery>();

        private readonly IChassisProvider _chassisProvider;

        public ChassisProviderTests()
        {
            _chassisProvider = new ChassisProvider(new SteelSubcontractor(), _getChassisRecipeQueryMock.Object);
        }

        [TestMethod]
        public void GetChassis_ShouldNotBeNull()
        {
            // Arrange
            var manufacturer = Manufacturer.Plandrover;
            var chassisRecipe = new ChassisRecipe(manufacturer, 1, 60, 1, 75, 2, 75);
            _getChassisRecipeQueryMock.Setup(query => query.Get(manufacturer)).Returns(chassisRecipe);

            // Act
            var result = _chassisProvider.GetChassis(manufacturer, 5);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetChassis_ShouldThrowEntityNotFoundException_WhenQueryReturnsNull()
        {
            // Arrange
            var manufacturer = Manufacturer.Plandrover;
            var chassisRecipe = new ChassisRecipe(manufacturer, 1, 60, 1, 75, 2, 75);
            _getChassisRecipeQueryMock.Setup(query => query.Get(manufacturer)).Returns((ChassisRecipe)null);

            // Act
            Action getChassisAction = () =>
            {
                _chassisProvider.GetChassis(manufacturer, 5);
            };

            // Assert
            getChassisAction.Should().Throw<EntityNotFoundException>()
                .WithMessage($"Unable to produce cars by manufacturer { manufacturer }");
        }

        [TestMethod]
        public void GetChassis_ShouldFail_WhenChasisFrontTypeNotFound()
        {
            // Arrange
            var manufacturer = Manufacturer.Plandrover;
            var chassisRecipe = new ChassisRecipe(manufacturer, -1, 60, 1, 75, 2, 75);
            _getChassisRecipeQueryMock.Setup(query => query.Get(manufacturer)).Returns(chassisRecipe);

            // Act
            Action getChassisAction = () =>
            {
                _chassisProvider.GetChassis(manufacturer, 5);
            };

            // Assert
            getChassisAction.Should().Throw<Exception>()
                .WithMessage("Unknown frontend type");
        }

        [TestMethod]
        public void GetChassis_ShouldFail_WhenChasisCabinTypeNotFound()
        {
            // Arrange
            var manufacturer = Manufacturer.Plandrover;
            var chassisRecipe = new ChassisRecipe(manufacturer, 1, 60, -1, 75, 2, 75);
            _getChassisRecipeQueryMock.Setup(query => query.Get(manufacturer)).Returns(chassisRecipe);

            // Act
            Action getChassisAction = () =>
            {
                _chassisProvider.GetChassis(manufacturer, 5);
            };

            // Assert
            getChassisAction.Should().Throw<Exception>()
                .WithMessage("Unknown cabin type");
        }

        [TestMethod]
        public void GetChassis_ShouldFail_WhenChasisBackTypeNotFound()
        {
            // Arrange
            var manufacturer = Manufacturer.Plandrover;
            var chassisRecipe = new ChassisRecipe(manufacturer, 1, 60, 1, 75, -1, 75);
            _getChassisRecipeQueryMock.Setup(query => query.Get(manufacturer)).Returns(chassisRecipe);

            // Act
            Action getChassisAction = () =>
            {
                _chassisProvider.GetChassis(manufacturer, 5);
            };

            // Assert
            getChassisAction.Should().Throw<Exception>()
                .WithMessage("Unknown trunk type");
        }

        [TestMethod]
        public void GetChassis_ShouldThrowInvalidOperationException_WhenInstallingMoreDoorsForTwoDoorChassisCabin()
        {
            // Arrange
            var manufacturer = Manufacturer.Planborghini;
            var chassisRecipe = new ChassisRecipe(manufacturer, 1, 60, 0, 75, -1, 75);
            _getChassisRecipeQueryMock.Setup(query => query.Get(manufacturer)).Returns(chassisRecipe);

            // Act
            Action getChassisAction = () =>
            {
                _chassisProvider.GetChassis(manufacturer, 5);
            };

            // Assert
            getChassisAction.Should().Throw<InvalidOperationException>()
                .WithMessage("This cabin type does not support the installation of another door");
        }
    }
}
