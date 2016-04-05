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
    public class FillupFacts
    {
        public class General
        {
            [Fact]
            public void BasicUsage()
            {
                // arrange
                Fillup f;
                f = new Fillup();
                f.Odometer = 200;
                f.Liters = 30m;

                // act

                // assert
                f.Odometer.ShouldEqual(200);
                f.Liters.ShouldEqual(30m);
            }
        }

        public class FillupProperty
        {
            [Fact]
            public void NullableKML()
            {
                Fillup f;
                f = new Fillup();
                f.Odometer = 200;
                f.Liters = 30m;

                f.KML.ShouldBeNull();
            }

            [Fact]
            public void KmTo12Liter()
            {
                Fillup f1;
                f1 = new Fillup();
                f1.Odometer = 1000;
                f1.Liters = 40m;

                Fillup f2 = new Fillup();
                f2.Odometer = 1600;
                f2.Liters = 50m;

                f1.NextFillup = f2;

                f2.KML.ShouldBeNull();
                f1.KML.ShouldEqual(12m);
            }

            [Fact]
            public void KmTo10Liter()
            {
                Fillup f1;
                f1 = new Fillup();
                f1.Odometer = 1600;
                f1.Liters = 50m;

                Fillup f2 = new Fillup();
                f2.Odometer = 2000;
                f2.Liters = 40m;

                f1.NextFillup = f2;

                f2.KML.ShouldBeNull();
                f1.KML.ShouldEqual(10m);
            }

            [Fact]
            public void Fill2AndAvg()
            {
                Fillup f1;
                f1 = new Fillup();
                f1.Odometer = 1000;
                f1.Liters = 40m;

                Fillup f2 = new Fillup();
                f2.Odometer = 1600;
                f2.Liters = 50m;

                Fillup f3 = new Fillup();
                f3.Odometer = 2000;
                f3.Liters = 40m;

                f1.NextFillup = f2;
                f2.NextFillup = f3;

                f1.KML.ShouldEqual(12m);
                f2.KML.ShouldEqual(10m);
                f3.KML.ShouldBeNull();
                Assert.Equal(11m,(f1.KML.Value + f2.KML.Value) / 2);
            }
        }
    }
}
