using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Domain.Engine.EngineSpecifications;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests
{
    [TestClass]
    public class EngineProviderTests
    {
        private readonly Mock<IGetEngineSpecificationQuery> _getEngineSpecificationQueryMock = new Mock<IGetEngineSpecificationQuery>();
        private readonly Mock<IGetPistons> _getPistonsMock = new Mock<IGetPistons>();
        private readonly IEngineProvider _engineProvider;

        public EngineProviderTests()
        {
            _engineProvider = new EngineProvider(_getPistonsMock.Object,
                new SteelSubcontractor(),
                _getEngineSpecificationQueryMock.Object,
                new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 }));
        }

        [TestMethod]
        public void GetEngine_ThrowsInvalidOperationException_WhenEngineNotFinished()
        {
            // Arrange
            var manufacturer = Manufacturer.Planborghini;
            var engineSpecification = new EngineSpecification
            {
                CylinderCount = 6,
                Name = "Gasoline V6",
                PropulsionType = Propulsion.Gasoline
            };

            _getEngineSpecificationQueryMock
                .Setup(query => query.GetForManufacturer(manufacturer))
                .Returns(engineSpecification);

            // so engine is unfinished
            _getPistonsMock
                .Setup(query => query.Get(engineSpecification.CylinderCount))
                .Returns(0);

            // Act
            Action getEngineAction = () =>
            {
                _engineProvider.GetEngine(manufacturer);
            };

            // Assert
            getEngineAction.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void EngineProvider_GetEngine()
        {
            // Arrange
            var manufacturer = Manufacturer.Planborghini;
            var engineSpecification = new EngineSpecification
            {
                CylinderCount = 6,
                Name = "Gasoline V6",
                PropulsionType = Propulsion.Gasoline
            };

            _getEngineSpecificationQueryMock
                .Setup(query => query.GetForManufacturer(manufacturer))
                .Returns(engineSpecification);

            // Act



            // Assert
        }
    }
}
