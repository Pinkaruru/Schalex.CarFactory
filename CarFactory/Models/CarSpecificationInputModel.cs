using CarFactory_Domain;
using static CarFactory.Controllers.CarController;

namespace CarFactory.Models
{
    public class CarSpecificationInputModel
    {
        public int NumberOfDoors { get; set; }
        public CarPaintSpecificationInputModel Paint { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
        public SpeakerSpecificationInputModel[] DoorSpeakers { get; set; }
    }
}
