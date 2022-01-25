using CarFactory_Assembly;
using CarFactory_Chasis;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using CarFactory_Paint;
using CarFactory_Storage;
using CarFactory_SubContractor;
using CarFactory_Wheels;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class CarFactoryTests
    {
        private readonly Mock<IChassisProvider> _chassisProviderMock = new Mock<IChassisProvider>();
        private readonly Mock<IEngineProvider> _engineProviderMock = new Mock<IEngineProvider>();
        private readonly Mock<IPainter> _painterMock = new Mock<IPainter>();
        private readonly Mock<IInteriorProvider> _interiorProviderMock = new Mock<IInteriorProvider>();
        private readonly Mock<IWheelProvider> _wheelProviderMock = new Mock<IWheelProvider>();
        private readonly Mock<ICarAssembler> _carAssemblerMock = new Mock<ICarAssembler>();
        private readonly ICarFactory _carFactory;

        public CarFactoryTests()
        {
            _carFactory = new CarFactory_Factory.CarFactory(_chassisProviderMock.Object,
                _engineProviderMock.Object,
                _painterMock.Object,
                _interiorProviderMock.Object,
                _wheelProviderMock.Object,
                _carAssemblerMock.Object);
        }

        [TestMethod]
        public void BuildCar_MustNotBeNull()
        {
            // Arrange
            var spec = GetValidSpec();
            var specs = new List<CarSpecification>() { spec };
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();
            var wheels = new Wheel[4];
            var car = new Car(chassis, engine, interior, wheels);

            _chassisProviderMock.Setup(x => x.GetChassis(spec.Manufacturer, spec.NumberOfDoors)).Returns(chassis);
            _engineProviderMock.Setup(x => x.GetEngine(spec.Manufacturer)).Returns(engine);
            _interiorProviderMock.Setup(x => x.GetInterior(spec)).Returns(interior);
            _wheelProviderMock.Setup(x => x.GetWheels()).Returns(wheels);
            _carAssemblerMock.Setup(x => x.AssembleCar(chassis, engine, interior, wheels)).Returns(car);

            car.PaintJob = spec.PaintJob;
            _painterMock.Setup(x => x.PaintCar(car, spec.PaintJob)).Returns(car);

            // Act
            var result = _carFactory.BuildCars(specs);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void BuildCar_MustBeExactAmountAsRequested()
        {
            // Arrange
            var spec = GetValidSpec();
            var specs = new List<CarSpecification>() { spec, GetValidSpec() };
            var chassis = new Chassis("", true, 0);
            var engine = new Engine(new EngineBlock(10), "Test");
            var interior = new Interior();
            var wheels = new Wheel[4];
            var car = new Car(chassis, engine, interior, wheels);

            _chassisProviderMock.Setup(x => x.GetChassis(spec.Manufacturer, spec.NumberOfDoors)).Returns(chassis);
            _engineProviderMock.Setup(x => x.GetEngine(spec.Manufacturer)).Returns(engine);
            _interiorProviderMock.Setup(x => x.GetInterior(spec)).Returns(interior);
            _wheelProviderMock.Setup(x => x.GetWheels()).Returns(wheels);
            _carAssemblerMock.Setup(x => x.AssembleCar(chassis, engine, interior, wheels)).Returns(car);

            car.PaintJob = spec.PaintJob;
            _painterMock.Setup(x => x.PaintCar(car, spec.PaintJob)).Returns(car);

            // Act
            var result = _carFactory.BuildCars(specs);

            // Assert
            result.Count().Should().Be(specs.Count());
        }

        private CarSpecification GetValidSpec()
        {
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

            return new CarSpecification(paintJob, Manufacturer.Plandrover, 5, doorSpeakers, dashboardSpeakers);
        }
    }
}
