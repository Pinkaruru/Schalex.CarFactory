using CarFactory.Controllers;
using CarFactory_Domain;
using CarFactory_Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarFactory.Controllers.CarController;

namespace UnitTests
{
    [TestClass]
    public class CarControllerTests
    {
        private readonly CarController _carController;
        private readonly Mock<ICarFactory> _carFactoryMock = new Mock<ICarFactory>();

        public CarControllerTests()
        {
            _carController = new CarController(_carFactoryMock.Object);
        }

        [TestMethod]
        public void Post_PaintTypesShouldAcceptMixedCasing()
        {
            // Arrange
            var carSpecs = GetCarSpecsWithCustomColors("StRiPe", "BLUE", "whITE");


            //_carFactoryMock.Setup(factory => factory.BuildCars()).Results();

            // Act
            var cars = _carController.Post(carSpecs);

            // Assert
        }

        private BuildCarInputModel GetCarSpecsWithCustomColors(string type, string baseColor, string stripeColor = null, string dotColor = null)
        {
            var doorSpeakers = new SpeakerSpecificationInputModel[]
                                    {
                                        new SpeakerSpecificationInputModel
                                        {
                                            IsSubwoofer = true
                                        }
                                    };

            var frontWindowSpeakers = new SpeakerSpecificationInputModel[]
                                    {
                                        new SpeakerSpecificationInputModel
                                        {
                                            IsSubwoofer = true
                                        }
                                    };

            var carSpec = new BuildCarInputModelItem()
            {
                Amount = 1,
                Specification = new CarSpecificationInputModel
                {
                    DoorSpeakers = doorSpeakers,
                    FrontWindowSpeakers = frontWindowSpeakers,
                    Manufacturer = Manufacturer.Plandrover,
                    NumberOfDoors = 5,
                    Paint = new CarPaintSpecificationInputModel
                    {
                        Type = type,
                        BaseColor = baseColor,
                        StripeColor = stripeColor,
                        DotColor = dotColor,
                    }
                }
            };

            var cars = new List<BuildCarInputModelItem>();

            return new BuildCarInputModel
            {
                Cars = new List<BuildCarInputModelItem>() { carSpec }
            };
        }
    }
}
