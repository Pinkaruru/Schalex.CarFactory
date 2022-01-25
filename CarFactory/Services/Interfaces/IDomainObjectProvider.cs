using CarFactory.Models;
using CarFactory_Factory;
using System.Collections.Generic;

namespace CarFactory.Services.Interfaces
{
    public interface IDomainObjectProvider
    {
        IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs);
    }
}
