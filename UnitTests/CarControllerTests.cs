using CarFactory.Controllers;
using CarFactory.Models;
using CarFactory.Services.Interfaces;
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
        /*
         * Only if time still allows
         */
        private readonly CarController _carController;
        private readonly Mock<ICarFactory> _carFactoryMock = new Mock<ICarFactory>();

        public CarControllerTests(IDomainObjectProvider domainModelProvider)
        {
            _carController = new CarController(_carFactoryMock.Object, domainModelProvider);
        }

        //[TestMethod]
        //public void Post_()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //}
    }
}
