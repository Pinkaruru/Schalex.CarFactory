using CarFactory_Chasis;
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
    public class ChassisWelderTests
    {
        private readonly ChassisWelder _chassisWelder;
        public ChassisWelderTests()
        {
            _chassisWelder = new ChassisWelder();
        }

        [TestMethod]
        public void StartWeld_ShouldThrowInvalidOperationException_WhenInstallingMoreThanOneDoorInChassisBack()
        {
            // Arrange
            int numberOfDoors = 2;
            var chassisPart = new ChassisBack(0);

            // Act
            Action startWeldAction = () =>
            {
                _chassisWelder.StartWeld(chassisPart, numberOfDoors);
            };

            // Assert
            startWeldAction.Should().Throw<InvalidOperationException>()
                .WithMessage("This cabin type does not support the installation of another door");
        }

        [TestMethod]
        public void StartWeld_ShouldReturnFalse_WhenGivenChassisFront()
        {
            // Arrange
            int numberOfDoors = 1;
            var chassisPart = new ChassisFront(0);

            // Act
            var result = _chassisWelder.StartWeld(chassisPart, numberOfDoors);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void StartWeld_ShouldReturnFalse_WhenGivenChassisCabin()
        {
            // Arrange
            int numberOfDoors = 1;
            var chassisPart = new ChassisCabin(0);

            // Act
            var result = _chassisWelder.StartWeld(chassisPart, numberOfDoors);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ContinueWeld_ShouldThrowInvalidOperationException_WhenInstallingMoreThanFourDoorInChassisBack()
        {
            // Arrange
            int numberOfDoors = 5;
            var chassisPart = new ChassisCabin(0);

            // Act
            Action startWeldingAction = () =>
            {
                _chassisWelder.ContinueWeld(chassisPart, numberOfDoors);
            };

            // Assert
            startWeldingAction.Should().Throw<InvalidOperationException>()
                .WithMessage("This cabin type does not support the installation of another door");
        }

        [TestMethod]
        public void ContinueWeld_ShouldReturnFalse_WhenGivenChassisFront()
        {
            // Arrange
            int numberOfDoors = 1;
            var chassisPart = new ChassisFront(0);

            // Act
            var result = _chassisWelder.ContinueWeld(chassisPart, numberOfDoors);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void ContinueWeld_ShouldReturnFalse_WhenGivenChassisBack()
        {
            // Arrange
            int numberOfDoors = 1;
            var chassisPart = new ChassisBack(0);

            // Act
            var result = _chassisWelder.ContinueWeld(chassisPart, numberOfDoors);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void FinishWeld_ShouldReturnFalse_WhenGivenChassisBack()
        {
            // Arrange
            var chassisPart = new ChassisBack(0);

            // Act
            var result = _chassisWelder.FinishWeld(chassisPart);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void FinishWeld_ShouldReturnFalse_WhenGivenChassisCabin()
        {
            // Arrange
            var chassisPart = new ChassisCabin(0);

            // Act
            var result = _chassisWelder.FinishWeld(chassisPart);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void GetChassis_ShouldNotBeNull()
        {
            // Arrange
            var chassisBack = new ChassisBack(0);
            var chassisCabin = new ChassisCabin(0);
            var chassisFront = new ChassisFront(0);
            var numberOfDoors = 3;

            // Act
            _chassisWelder.StartWeld(chassisBack, 1);
            _chassisWelder.ContinueWeld(chassisCabin, --numberOfDoors);
            _chassisWelder.FinishWeld(chassisFront);

            var chassis = _chassisWelder.GetChassis();

            // Assert
            chassis.Should().NotBeNull();
        }

        [TestMethod]
        public void GetChassis_ChassisShouldHaveSameAmountOfDoorsAsDoorsGiven()
        {
            // Arrange
            var chassisBack = new ChassisBack(0);
            var chassisCabin = new ChassisCabin(0);
            var chassisFront = new ChassisFront(0);
            var numberOfDoors = 3;

            // Act
            _chassisWelder.StartWeld(chassisBack, 1);
            _chassisWelder.ContinueWeld(chassisCabin, numberOfDoors - 1);
            _chassisWelder.FinishWeld(chassisFront);

            var chassis = _chassisWelder.GetChassis();

            // Assert
            chassis.DoorAmount.Should().Be(numberOfDoors);
        }

        [TestMethod]
        public void GetChassis_DescriptionShouldMatchGivenChassises()
        {
            // Arrange
            var chassisBack = new ChassisBack(0);
            var chassisCabin = new ChassisCabin(0);
            var chassisFront = new ChassisFront(0);
            var numberOfDoors = 3;

            // Act
            _chassisWelder.StartWeld(chassisBack, 1);
            _chassisWelder.ContinueWeld(chassisCabin, numberOfDoors - 1);
            _chassisWelder.FinishWeld(chassisFront);

            var chassis = _chassisWelder.GetChassis();

            // Assert
            chassis.Description.Should().Be($"{chassisCabin.GetChassisType()} {chassisBack.GetChassisType()} {chassisFront.GetChassisType()}");
        }

        [TestMethod]
        public void GetChassis_ChassisShouldBeValid()
        {
            // Arrange
            var chassisBack = new ChassisBack(0);
            var chassisCabin = new ChassisCabin(0);
            var chassisFront = new ChassisFront(0);
            var numberOfDoors = 3;

            // Act
            _chassisWelder.StartWeld(chassisBack, 1);
            _chassisWelder.ContinueWeld(chassisCabin, numberOfDoors - 1);
            _chassisWelder.FinishWeld(chassisFront);

            var chassis = _chassisWelder.GetChassis();

            // Assert
            chassis.ValidConstruction.Should().BeTrue();
        }
    }
}
