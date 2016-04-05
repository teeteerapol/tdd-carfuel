using CarFuel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.DataAccess
{
    public interface ICarDb
    {
        IEnumerable<Car> GetAll(Func<Car, Boolean> predicate);
        Car Get(Guid id);
        Car Add(Car item);
    }
}
