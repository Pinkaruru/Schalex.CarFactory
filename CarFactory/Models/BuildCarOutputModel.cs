using CarFactory_Domain;
using System.Collections.Generic;

namespace CarFactory.Models
{
    public class BuildCarOutputModel
    {
        public long RunTime { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
