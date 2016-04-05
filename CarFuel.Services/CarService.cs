﻿using CarFuel.DataAccess;
using CarFuel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Services
{
    public class CarService
    {
        public ICarDb CarDb { get; }

        public CarService(ICarDb carDb)
        {
            if (carDb == null)
            {
                throw new ArgumentNullException(nameof(carDb));
            }
            CarDb = carDb;
        }

        public IEnumerable<Car> GetCarsByMember(Guid userId)
        {
            return CarDb.GetAll(c => c.OwnerId == userId);
        }

        public Car AddCar(Car car, Guid userId)
        {
            car.OwnerId = userId;
            return CarDb.Add(car);
        }

        public Boolean CanAddMoreCars(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
