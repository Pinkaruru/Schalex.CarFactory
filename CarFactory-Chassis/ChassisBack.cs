using System;

namespace CarFactory_Chasis
{
    public class ChassisBack : ChassisPart, IChassisDoorInstallable
    {
        public int DoorAmount { get; private set; } = 0;

        public ChassisBack(int typeId) : base(typeId)
        {}

        public override string GetChassisType()
        {
            switch (_typeId)
            {
                case 0:
                    return "Sedan";
                case 1:
                    return "Pickup";
                case 2:
                    return "Hatchback";
                default:
                    throw new Exception("Unknown trunk type");
            }
        }

        public override string GetType()
        {
            return "ChassisBack";
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
            if(DoorAmount >= 1)
            {
                return false;
            }

            return true;
        }
    }
}
