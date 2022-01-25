using CarFactory.Models;
using CarFactory.Services.Interfaces;
using CarFactory_Domain;
using CarFactory_Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CarFactory.Services
{
    public class DomainObjectProvider : IDomainObjectProvider
    {
        public IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            var wantedCars = new List<CarSpecification>();
            foreach (var spec in carsSpecs.Cars)
            {
                for (var i = 1; i <= spec.Amount; i++)
                {
                    if (spec.Specification.NumberOfDoors % 2 == 0)
                    {
                        throw new ArgumentException("Must give an odd number of doors");
                    }

                    PaintJob? paint = null; // TODO: If I find time, I will address nullable warnings https://docs.microsoft.com/en-us/dotnet/csharp/nullable-migration-strategies
                    spec.Specification.Paint = RemoveCasingsFromCarPaintSpecificationInputModel(spec.Specification.Paint);
                    var baseColor = Color.FromName(spec.Specification.Paint.BaseColor);

                    switch (spec.Specification.Paint.Type)
                    {
                        case "single":
                            paint = new SingleColorPaintJob(baseColor);
                            break;
                        case "stripe":
                            paint = new StripedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.StripeColor));
                            break;
                        case "dot":
                            paint = new DottedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.DotColor));
                            break;
                        default:
                            throw new ArgumentException(string.Format("Unknown paint type %", spec.Specification.Paint.Type));
                    }

                    var dashboardSpeakers = spec.Specification.FrontWindowSpeakers.Select(s => new CarSpecification.SpeakerSpecification { IsSubwoofer = s.IsSubwoofer });
                    var doorSpeakers = spec.Specification.DoorSpeakers.Select(S => new CarSpecification.SpeakerSpecification { IsSubwoofer = S.IsSubwoofer });
                    var wantedCar = new CarSpecification(paint, spec.Specification.Manufacturer, spec.Specification.NumberOfDoors, doorSpeakers, dashboardSpeakers);
                    wantedCars.Add(wantedCar);
                }
            }
            return wantedCars;
        }

        private CarPaintSpecificationInputModel RemoveCasingsFromCarPaintSpecificationInputModel(CarPaintSpecificationInputModel model)
        {
            model.Type = model.Type != null ? model.Type.ToLower() : string.Empty;
            model.BaseColor = model.BaseColor != null ? model.BaseColor.ToLower() : string.Empty;
            model.StripeColor = model.StripeColor != null ? model.StripeColor.ToLower() : string.Empty;
            model.DotColor = model.DotColor != null ? model.DotColor.ToLower() : string.Empty;

            return model;
        }
    }
}
