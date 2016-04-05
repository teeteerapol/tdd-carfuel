using CarFuel.Model;
using CarFuel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should;
using CarFuel.DataAccess;

namespace CarFuel.Facts.Services
{
    public class CarServiceFacts
    {
        public class AddCarMethods
        {
            [Fact]
            public void AddSingleCar()
            {
                var db = new FakeCarDb();
                var s = new CarService(db);
                var c = new Car();
                c.Make = "Honda";
                c.Model = "Civic";
                var userId = Guid.NewGuid();

                var c2 = s.AddCar(c, userId);

                c2.ShouldNotBeNull();
                c2.Make.ShouldEqual(c.Make);
                c2.Model.ShouldEqual(c.Model);

                var n = s.GetCarsByMember(userId);
                n.Count().ShouldEqual(1);
                Assert.Contains(n, x => x.OwnerId == userId);
            }
        }

        public class GetCarsByMemberMethods
        {

        }

        public class CanAddMoreCarsMethods
        {

        }
    }
}
