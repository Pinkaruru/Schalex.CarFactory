using System.Collections.Generic;

namespace CarFactory_SubContractor
{
    public interface ISteelSubcontractor
    {
        List<SteelDelivery> OrderSteel(int amount);
    }
}
