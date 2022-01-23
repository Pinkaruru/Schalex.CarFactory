using CarFactory_Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CarFactory_Factory
{
    public class CarFactory : ICarFactory
    {
        private IChassisProvider _chassisProvider;
        private IEngineProvider _engineProvider;
        private IPainter _painter;
        private IInteriorProvider _interiorProvider;
        private IWheelProvider _wheelProvider;
        private ICarAssembler _carAssembler;

        public CarFactory(
            IChassisProvider chassisProvider, 
            IEngineProvider engineProvider, 
            IPainter painter, 
            IInteriorProvider interiorProvider, 
            IWheelProvider wheelProvider, 
            ICarAssembler carAssembler)
        {
            _chassisProvider = chassisProvider;
            _engineProvider = engineProvider;
            _painter = painter;
            _interiorProvider = interiorProvider;
            _wheelProvider = wheelProvider;
            _carAssembler = carAssembler;
        }

        public IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs)
        {
            var cars = new List<Car>();
            foreach(var spec in specs)
            {
                var chassis = _chassisProvider.GetChassis(spec.Manufacturer, spec.NumberOfDoors);
                var engine = _engineProvider.GetEngine(spec.Manufacturer); // TODO: optimize
                var interior = _interiorProvider.GetInterior(spec); 
                var wheels = _wheelProvider.GetWheels();
                var car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);
                var paintedCar = _painter.PaintCar(car, spec.PaintJob); // TODO: optimize
                cars.Add(paintedCar);
            }
            return cars;
        }

        public double GetAveragePaintPerformance(IEnumerable<CarSpecification> specs)
        {
            /*
             * Original average between 2.3 and 3.0 with 100 iterations
             */
            long total = 0L;
            var stopWatch = new Stopwatch();
            foreach(var spec in specs)
            {
                var chassis = _chassisProvider.GetChassis(spec.Manufacturer, spec.NumberOfDoors);
                var engine = _engineProvider.GetEngine(spec.Manufacturer);
                var interior = _interiorProvider.GetInterior(spec);
                var wheels = _wheelProvider.GetWheels();
                var car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);

                stopWatch.Start();
                _painter.PaintCar(car, spec.PaintJob);
                stopWatch.Stop();

                total =+ stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();
            }

            return (double) total / specs.Count();
        }
    }
}