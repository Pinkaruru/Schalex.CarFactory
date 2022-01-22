using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public abstract class ChassisPart
    {
        public readonly int _typeId;
        public ChassisPart(int typeId)
        {
            _typeId = typeId;
        }

        // TODO: Add a new class inherited from Chassispart that can hold the doors (see chassisprovider.cs to weld doors)

        public new abstract string GetType();

        public abstract string GetChassisType();
    }

    
}
