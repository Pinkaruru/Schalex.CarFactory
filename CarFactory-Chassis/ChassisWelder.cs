using CarFactory_Domain;
using System;

namespace CarFactory_Chasis
{
    public class ChassisWelder
    {
        private ChassisPart _firstPart;
        private ChassisPart _secondPart;
        private ChassisPart _thirdPart;
        public bool StartWeld(ChassisPart firstPart, int numberOfDoors)
        {
            if(_firstPart == null)
            {
                _firstPart = firstPart;
            }

            if(_firstPart is ChassisBack)
            {
                for (var i = 0; i != numberOfDoors; i++)
                {
                    ((ChassisBack)_firstPart).InstallDoor();
                }

                return true;
            }

            return false;
        }

        public bool ContinueWeld(ChassisPart secondPart, int numberOfDoors)
        {
            if (_secondPart == null)
            {
                _secondPart = secondPart;
            }

            if (_secondPart is ChassisCabin)
            {
                for (var i = 0; i != numberOfDoors; i++)
                {
                    ((ChassisCabin)_secondPart).InstallDoor();
                }

                return true;
            }

            return false;
        }

        public bool FinishWeld(ChassisPart thirdPart)
        {
            if (_thirdPart == null)
            {
                _thirdPart = thirdPart;
                return true;
            }
            return false;
        }

        public Chassis GetChassis()
        {
            if(_firstPart == null || _secondPart == null || _thirdPart == null)
            {
                throw new Exception("Chassis not finished");
            }

            var isValid = false;
            if(_firstPart.GetType() == "ChassisBack" && _secondPart.GetType() == "ChassisCabin" && _thirdPart.GetType() == "ChassisFront")
            {
                isValid = true;
            }

            var description = _secondPart.GetChassisType()+ " " + _firstPart.GetChassisType() + " " + _thirdPart.GetChassisType();

            // A type check has already happened at this point, which is why I am casting without checking here
            var doorTotalAmount = ((ChassisBack)_firstPart).DoorAmount + ((ChassisCabin)_secondPart).DoorAmount;
            
            return new Chassis(description, isValid, doorTotalAmount);
        }
    }
}
