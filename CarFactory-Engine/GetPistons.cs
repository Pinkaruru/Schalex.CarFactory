using CarFactory.Utilities;

namespace CarFactory_Engine
{
    public class GetPistons : IGetPistons
    {
        public int Get(int amount)
        {
            SlowWorker.FakeWorkingForMillis(amount * 50);
            return amount;
        }
    }
}
