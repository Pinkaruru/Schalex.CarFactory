using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class InteriorProviderTests
    {
        private readonly IInteriorProvider _interiorProvider;

        public InteriorProviderTests()
        {
            _interiorProvider = new InteriorProvider(new DashboardBuilder(), new SeatBuilder(), new SpeakerBuilder());
        }
    }
}
