using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    /// <summary>
    /// Indicates whether doors are installable in ChassisParts
    /// </summary>
    internal interface IChassisDoorInstallable
    {
        void InstallDoor();

        /// <summary>
        /// To find out wheter you can install another door or not
        /// </summary>
        /// <returns>A boolean indicating whether another door is installable or not</returns>
        bool AnotherDoorInstallable();
    }
}
