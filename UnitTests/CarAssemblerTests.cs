using CarFactory_Assembly;
using CarFactory_Chasis;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using CarFactory_Storage;
using CarFactory_SubContractor;
using CarFactory_Wheels;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests
{
    [TestClass]
    public class CarAssemblerTests
    {
        private readonly ICarAssembler _carAssembler;
        private readonly IChassisProvider _chassisProvider;
        private readonly IEngineProvider _engineProvider;
        private readonly IInteriorProvider _interiorProvider;
        private readonly IWheelProvider _wheelProvider;

        public CarAssemblerTests()
        {
            _carAssembler = new CarAssembler();
        }

        [TestMethod]
        public void AssembleCar_ShouldThrowArgumentNullException_WhenChassisIsNull()
        {
            // Arrange
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();
            var wheels = new Wheel[4];

            // Act
            Action assembleCarAction = () =>
            {
                _carAssembler.AssembleCar(null, engine, interior, wheels);
            };

            // Assert
            assembleCarAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AssembleCar_ShouldThrowArgumentNullException_WhenEngineIsNull()
        {
            // Arrange
            var chassis = new Chassis("", true, 0);
            var interior = new Interior();
            var wheels = new Wheel[4];

            // Act
            Action assembleCarAction = () =>
            {
                _carAssembler.AssembleCar(chassis, null, interior, wheels);
            };

            // Assert
            assembleCarAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AssembleCar_ShouldThrowArgumentNullException_WhenInteriorIsNull()
        {
            // Arrange
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var wheels = new Wheel[4];

            // Act
            Action assembleCarAction = () =>
            {
                _carAssembler.AssembleCar(chassis, engine, null, wheels);
            };

            // Assert
            assembleCarAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AssembleCar_ShouldThrowArgumentNullException_WhenWheelsAreNull()
        {
            // Arrange
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();

            // Act
            Action assembleCarAction = () =>
            {
                _carAssembler.AssembleCar(chassis, engine, interior, null);
            };

            // Assert
            assembleCarAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AssembleCar_ShouldFail_WhenWheelsAreBiggerThan4()
        {
            // Arrange
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();
            var wheels = new Wheel[5];

            // Act
            Action assembleCarAction = () =>
            {
                _carAssembler.AssembleCar(chassis, engine, interior, wheels);
            };

            // Assert
            assembleCarAction.Should()
                .Throw<Exception>()
                .WithMessage("Common cars must have 4 wheels");
        }

        [TestMethod]
        public void AssembleCar_ShouldFail_WhenWheelsAreLessThan4()
        {
            // Arrange
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();
            var wheels = new Wheel[3];

            // Act
            Action assembleCarAction = () =>
            {
                _carAssembler.AssembleCar(chassis, engine, interior, wheels);
            };

            // Assert
            assembleCarAction.Should()
                .Throw<Exception>()
                .WithMessage("Common cars must have 4 wheels");
        }

        [TestMethod]
        public void AssembleCar_ShouldNotBeNull_WithValidInput()
        {
            // Arrange
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();
            var wheels = new Wheel[4];

            // Act
            var car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);

            // Assert
            car.Should().NotBeNull();
        }
    }
}
