using CarFactory_Chasis;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public void GetChassis()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
