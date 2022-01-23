using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using CarFactory_Domain;
using CarFactory_Domain.Exceptions;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarFactory _carFactory;
        public CarController(ICarFactory carFactory)
        {
            _carFactory = carFactory;
        }

        [ProducesResponseType(typeof(BuildCarOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        public object Post([FromBody][Required] BuildCarInputModel carsSpecs)
        {
            try
            {
                var wantedCars = TransformToDomainObjects(carsSpecs);

                //var result = _carFactory.GetAveragePaintPerformance(wantedCars);

                //Build cars
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var cars = _carFactory.BuildCars(wantedCars);
                stopwatch.Stop();

                //Create response and return
                return new BuildCarOutputModel
                {
                    Cars = cars,
                    RunTime = stopwatch.ElapsedMilliseconds
                };
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorModel
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = e.Message
                });
            }
            catch (Exception)
            {
                // I know. I added this as a dummy for global error handling
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorModel 
                { 
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Sorry, something went wrong. It's not you, it's us. If you want, you can inform us about the problem at carfactory@planday.com."
                });
            }
        }

        private static IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            var wantedCars = new List<CarSpecification>();
            foreach (var spec in carsSpecs.Cars)
            {
                for(var i = 1; i <= spec.Amount; i++)
                {
                    if(spec.Specification.NumberOfDoors % 2 == 0)
                    {
                        throw new ArgumentException("Must give an odd number of doors");
                    }

                    PaintJob? paint = null; // TODO: If I find time, I will address nullable warnings https://docs.microsoft.com/en-us/dotnet/csharp/nullable-migration-strategies
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

        public class BuildCarInputModel
        {
            public IEnumerable<BuildCarInputModelItem> Cars { get; set; }
        }

        public class BuildCarInputModelItem
        {
            [Required]
            public int Amount { get; set; }
            [Required]
            public CarSpecificationInputModel Specification { get; set; }
        }

        public class CarPaintSpecificationInputModel
        {
            public string Type { get; set; }
            public string BaseColor { get; set; }
            public string? StripeColor { get; set; }
            public string? DotColor { get; set; }
        }

        public class CarSpecificationInputModel
        {
            public int NumberOfDoors { get; set; }
            public CarPaintSpecificationInputModel Paint { get; set; }
            public Manufacturer Manufacturer { get; set; }
            public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
            public SpeakerSpecificationInputModel[] DoorSpeakers { get; set; }
        }

        public class SpeakerSpecificationInputModel
        {
            public bool IsSubwoofer { get; set; }
        }

        public class BuildCarOutputModel{
            public long RunTime { get; set; }
            public IEnumerable<Car> Cars { get; set; }
        }

        public class ErrorModel
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
