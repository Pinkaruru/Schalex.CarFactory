using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisCabin : ChassisPart, IChassisDoorInstallable
    {
        public int DoorAmount { get; private set; } = 0;

        public ChassisCabin(int typeId) : base(typeId)
        {

        }

        public override string GetChassisType()
        {
            switch (_typeId)
            {
                case 0:
                    return "Two Door";
                case 1:
                    return "Four Door";
                default:
                    throw new Exception("Unknown cabin type");
            }
        }

        public override string GetType()
        {
            return "ChassisCabin";
        }

        public void InstallDoor()
        {
            if (!AnotherDoorInstallable())
            {
                throw new InvalidOperationException("This cabin type does not support the installation of another door");
            }

            DoorAmount++;
        }

        public bool AnotherDoorInstallable()
        {
            if (_typeId == 0 && DoorAmount >= 2)
            {
                return false;
            }

            if (_typeId == 1 && DoorAmount >= 4)
            {
                return false;
            }

            return true;
        }
    }

}
