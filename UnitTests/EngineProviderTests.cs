using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class EngineProviderTests
    {
        private readonly Mock<IGetEngineSpecificationQuery> _getEngineSpecificationQueryMock = new Mock<IGetEngineSpecificationQuery>();
        private readonly Mock<IGetPistons> _getPistonsMock = new Mock<IGetPistons>();
        private readonly Mock<ISteelSubcontractor> _steelSubcontractorMock = new Mock<ISteelSubcontractor>();

        private readonly IEngineProvider _engineProvider;

        public EngineProviderTests()
        {
            _engineProvider = new EngineProvider(_getPistonsMock.Object, 
                _steelSubcontractorMock.Object, 
                _getEngineSpecificationQueryMock.Object,
                new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });
        }
    }
}
