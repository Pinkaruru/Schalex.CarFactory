using CarFactory.Models;
using CarFactory.Services;
using CarFactory.Services.Interfaces;
using CarFactory_Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class DomainObjectProviderTests
    {
        private readonly IDomainObjectProvider _domainModelProvider;

        public DomainObjectProviderTests()
        {
            _domainModelProvider = new DomainObjectProvider();
        }

        [TestMethod]
        public void TransformToDomainObjects_PaintTypesShouldAcceptMixedCasing_Type()
        {
            // Arrange
            var carSpecsInputModel = GetCarSpecsWithCustomColors("StrIPE", "BLUE", "reD");

            // Act
            var carSpecs = _domainModelProvider.TransformToDomainObjects(carSpecsInputModel);

            // Assert
            carSpecs.Select(cs => cs.PaintJob).OfType<StripedPaintJob>().Count().Should().Be(carSpecs.Count());
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
