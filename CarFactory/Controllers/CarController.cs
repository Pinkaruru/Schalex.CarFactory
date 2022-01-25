using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using CarFactory.Models;
using CarFactory.Services.Interfaces;
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
        private readonly IDomainObjectProvider _domainModelProvider;

        public CarController(ICarFactory carFactory,
            IDomainObjectProvider domainModelProvider)
        {
            _carFactory = carFactory;
            _domainModelProvider = domainModelProvider;
        }

        [ProducesResponseType(typeof(BuildCarOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        public object Post([FromBody][Required] BuildCarInputModel carsSpecs)
        {
            try
            {
                var wantedCars = _domainModelProvider.TransformToDomainObjects(carsSpecs);

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
    }
}
