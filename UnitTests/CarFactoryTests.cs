using CarFactory_Assembly;
using CarFactory_Chasis;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using CarFactory_Paint;
using CarFactory_Storage;
using CarFactory_SubContractor;
using CarFactory_Wheels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class CarFactoryTests
    {
        private IChassisProvider _chassisProvider;
        private IEngineProvider _engineProvider;
        private IPainter _painter;
        private IInteriorProvider _interiorProvider;
        private IWheelProvider _wheelProvider;
        private ICarAssembler _carAssembler;
        private ICarFactory _carFactory;

        private readonly Mock<IGetChassisRecipeQuery> _getChassisRecipeQueryMock = new Mock<IGetChassisRecipeQuery>();
        private readonly Mock<IGetEngineSpecificationQuery> _getEngineSpecificationQueryMock = new Mock<IGetEngineSpecificationQuery>();
        private readonly Mock<IGetRubberQuery> _getRubberQueryMock = new Mock<IGetRubberQuery>();
        private readonly Mock<IGetPistons> _getPistonsMock = new Mock<IGetPistons>();
        private readonly Mock<ISteelSubcontractor> _steelSubcontractorMock = new Mock<ISteelSubcontractor>();

        public CarFactoryTests()
        {
            _chassisProvider = new ChassisProvider(_steelSubcontractorMock.Object, _getChassisRecipeQueryMock.Object);
            _engineProvider = new EngineProvider(_getPistonsMock.Object, _steelSubcontractorMock.Object, _getEngineSpecificationQueryMock.Object, new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 }));
            _painter = new Painter();
            _interiorProvider = new InteriorProvider(new DashboardBuilder(), new SeatBuilder(), new SpeakerBuilder());
            _wheelProvider = new WheelProvider(_getRubberQueryMock.Object);
            _carAssembler = new CarAssembler();
            _carFactory = new CarFactory_Factory.CarFactory(_chassisProvider, _engineProvider, _painter, _interiorProvider, _wheelProvider, _carAssembler);
        }
    }
}
