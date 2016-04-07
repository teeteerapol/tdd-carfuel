using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using Xunit;
using CarFuel.Model;

namespace CarFuel.Facts
{
    public class CarFacts
    {
        public class General
        {
            [Fact]
            public void BasicUsage()
            {
                // arrange
                Car c = new Car();
                c.Make = "Honda";
                c.Model = "City";
                // act

                // assert
                c.Make.ShouldEqual("Honda");
                c.Model.ShouldEqual("City");
                c.ListFillup.ShouldNotBeNull();
                c.ListFillup.Count.ShouldEqual(3);
            }
        }

        public class AddFillupMethods
        {
            [Fact]
            public void SingleFillup()
            {
                Car c = new Car();
                c.Make = "Honda";
                c.Model = "City";

                Fillup f = c.AddFillUp(1000, 40m);

                f.ShouldNotBeNull();
                f.Odometer.ShouldEqual(1000);
                f.Liters.ShouldEqual(40m);
                c.ListFillup.Count.ShouldEqual(1);
            }

            [Fact]
            public void TwoFillups()
            {
                Car c = new Car();
                c.Make = "Honda";
                c.Model = "City";

                Fillup f1 = c.AddFillUp(1000, 40m);
                Fillup f2 = c.AddFillUp(1600, 50m);

                f1.ShouldNotBeNull();
                f1.Odometer.ShouldEqual(1000);
                f1.Liters.ShouldEqual(40m);

                f2.ShouldNotBeNull();
                f2.Odometer.ShouldEqual(1600);
                f2.Liters.ShouldEqual(50m);

                //Assert.Same(f1.NextFillup, f2);
                f1.NextFillup.ShouldBeSameAs(f2);
                c.ListFillup.Count.ShouldEqual(2);
                c.ListFillup.ToList()[0].NextFillup.ShouldNotBeNull();
                c.ListFillup.ToList()[0].KML.ShouldEqual(12m);
                c.ListFillup.ToList()[1].NextFillup.ShouldBeNull();
            }

            [Fact]
            public void ThreeFillups()
            {
                Car c = new Car();
                c.Make = "Honda";
                c.Model = "City";

                Fillup f1 = c.AddFillUp(1000, 40m);
                Fillup f2 = c.AddFillUp(1600, 50m);
                Fillup f3 = c.AddFillUp(2000, 40m);

                f1.ShouldNotBeNull();
                f1.Odometer.ShouldEqual(1000);
                f1.Liters.ShouldEqual(40m);

                f2.ShouldNotBeNull();
                f2.Odometer.ShouldEqual(1600);
                f2.Liters.ShouldEqual(50m);

                f3.ShouldNotBeNull();
                f3.Odometer.ShouldEqual(2000);
                f3.Liters.ShouldEqual(40m);

                //Assert.Same(f1.NextFillup, f2);
                f1.NextFillup.ShouldBeSameAs(f2);
                c.ListFillup.Count.ShouldEqual(3);
                c.ListFillup.ToList()[0].NextFillup.ShouldNotBeNull();
                c.ListFillup.ToList()[0].KML.ShouldEqual(12m);

                //Assert.Same(f2.NextFillup, f3);
                f2.NextFillup.ShouldBeSameAs(f3);
                c.ListFillup.ToList()[1].NextFillup.ShouldNotBeNull();
                c.ListFillup.ToList()[1].KML.ShouldEqual(10m);
                c.ListFillup.ToList()[2].NextFillup.ShouldBeNull();
            }
        }

        public class AvgProperty
        {
            [Fact]
            public void AvgKML()
            {
                Car c = new Car();
                c.Make = "Honda";
                c.Model = "City";

                Fillup f1 = c.AddFillUp(1000, 40m);
                Fillup f2 = c.AddFillUp(1600, 50m);
                Fillup f3 = c.AddFillUp(2000, 40m);

                c.AverageKmL.ShouldEqual(11.11m);
            }
        }
    }
}
