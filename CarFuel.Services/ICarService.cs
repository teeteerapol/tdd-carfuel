using System;
using System.Collections.Generic;
using CarFuel.Model;

namespace CarFuel.Services
{
    public interface ICarService
    {
        Car AddCar(Car car, Guid userId);
        bool CanAddMoreCars(Guid userId);
        IEnumerable<Car> GetCarsByMember(Guid userId);
    }
}