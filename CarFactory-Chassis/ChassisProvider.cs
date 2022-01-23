using CarFactory_Domain;
using CarFactory_Domain.Exceptions;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactory_Chasis
{
    public class ChassisProvider : IChassisProvider
    {
        private readonly ISteelSubcontractor _steelSubcontractor;
        private readonly IGetChassisRecipeQuery _chassisRecipeQuery;

        public ChassisProvider(ISteelSubcontractor steelSubcontractor, IGetChassisRecipeQuery chassisRecipeQuery)
        {
            _steelSubcontractor = steelSubcontractor;
            _chassisRecipeQuery = chassisRecipeQuery;
        }
        public Chassis GetChassis(Manufacturer manufacturer, int numberOfDoors)
        {
            var chassisRecipe = _chassisRecipeQuery.Get(manufacturer);

            if (chassisRecipe == null)
            {
                throw new EntityNotFoundException($"Unable to produce cars by manufacturer { manufacturer }");
            }

            var chassisParts = new List<ChassisPart>();
            chassisParts.Add(new ChassisBack(chassisRecipe.BackId));
            chassisParts.Add(new ChassisCabin(chassisRecipe.CabinId));
            chassisParts.Add(new ChassisFront(chassisRecipe.FrontId));

            CheckChassisParts(chassisParts);

            SteelInventory += _steelSubcontractor.OrderSteel(chassisRecipe.Cost).Select(d => d.Amount).Sum();
            CheckForMaterials(chassisRecipe.Cost);
            SteelInventory -= chassisRecipe.Cost;

            var chassisWelder = new ChassisWelder();

            /*
             * DevNotes: 
             * As explained in the README.md, door installation was written under the assumption that the uneven number of doors is explained that one door is the trunk door
             */

            chassisWelder.StartWeld(chassisParts[0], 1);
            chassisWelder.ContinueWeld(chassisParts[1], --numberOfDoors);
            chassisWelder.FinishWeld(chassisParts[2]);
 
            return chassisWelder.GetChassis();
        }

        public int SteelInventory { get; private set; }

        private void CheckForMaterials(int cost)
        {
            if (SteelInventory < cost)
            {
                throw new Exception("Not enough chassis material");
            }
        }

        private void CheckChassisParts(List<ChassisPart> parts)
        {
            if (parts == null)
            {
                throw new Exception("No chassis parts");
            }

            if (parts.Count > 3)
            {
                throw new Exception("Chassis parts missing");
            }

            if (parts.Count < 3)
            {
                throw new Exception("Too many chassis parts");
            }
        }
    }
}
