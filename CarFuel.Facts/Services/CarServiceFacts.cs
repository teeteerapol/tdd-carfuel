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
using Xunit.Abstractions;
using Moq;

namespace CarFuel.Facts.Services
{
    public class SharedService
    {
        public CarService CarService { get; set; }
        public FakeCarDb Db { get; set; }
        public SharedService()
        {
            Db = new FakeCarDb();
            CarService = new CarService(Db);
        }
    }

    [CollectionDefinition("collection1")]
    public class CarServiceFactsCollection : ICollectionFixture<SharedService> { }

    public class CarServiceFacts
    {
        [Collection("collection1")]
        public class AddCarMethods
        {
            //private ICarDb Db;
            private CarService Service;
            public FakeCarDb Db { get; set; }
            private ITestOutputHelper Output;

            public AddCarMethods(ITestOutputHelper output, SharedService s)
            {
                //Db = new FakeCarDb();
                //Service = new CarService(Db);
                Service = s.CarService;
                Db = s.Db;
                this.Output = output;

                this.Output.WriteLine("ctor");
            }

            [Fact]
            public void AddSingleCar()
            {
                this.Output.WriteLine("AddSingleCar");
                var c = new Car();
                c.Make = "Honda";
                c.Model = "Civic";
                var userId = Guid.NewGuid();

                Db.AddMethodHasCalled = false;

                var c2 = Service.AddCar(c, userId);

                c2.ShouldNotBeNull();
                c2.Make.ShouldEqual(c.Make);
                c2.Model.ShouldEqual(c.Model);

                Db.AddMethodHasCalled.ShouldBeTrue();

                //var n = Service.GetCarsByMember(userId);
                //n.Count().ShouldEqual(1);
                //Assert.Contains(n, x => x.OwnerId == userId);
            }

            [Fact]
            public void AddSingleCarMoq()
            {
                var mock = new Mock<ICarDb>();

                mock.Setup(db => db.Add(It.IsAny<Car>()))
                    .Returns((Car car) => car);

                var service = new CarService(mock.Object);

                var c = new Car();
                c.Make = "Honda";
                c.Model = "Civic";
                var userId = Guid.NewGuid();

                var c2 = service.AddCar(c, userId);

                c2.ShouldNotBeNull();
                c2.Make.ShouldEqual(c.Make);
                c2.Model.ShouldEqual(c.Model);
                mock.Verify(db => db.Add(It.IsAny<Car>()), Times.Once);
            }

            [Fact]
            public void Add3Cars_ThrowsException()
            {
                this.Output.WriteLine("Add3Car");
                var memberId = Guid.NewGuid();
                Service.AddCar(new Car(), memberId);
                Service.AddCar(new Car(), memberId);

                var ex = Assert.Throws<OverQuotaException>(() =>
                {
                    Service.AddCar(new Car(), memberId);
                });

                ex.Message.ShouldEqual("Cannot add more car.");
            }
        }

        [Collection("collection1")]
        public class GetCarsByMemberMethods
        {
            private CarService Service;
            public GetCarsByMemberMethods(SharedService s)
            {
                Service = s.CarService;
            }

            [Fact]
            public void MemberCanGetOnlyHisOrHerOwnCars()
            {
                //var db = new FakeCarDb();
                //var s = new CarService(db);

                var member1_Id = Guid.NewGuid();
                var member2_Id = Guid.NewGuid();
                var member3_Id = Guid.NewGuid();

                Service.AddCar(new Car(), member1_Id);

                Service.AddCar(new Car(), member2_Id);
                Service.AddCar(new Car(), member2_Id);

                Service.GetCarsByMember(member1_Id).Count().ShouldEqual(1);
                Service.GetCarsByMember(member2_Id).Count().ShouldEqual(2);
                Service.GetCarsByMember(member3_Id).Count().ShouldEqual(0);
            }
        }

        [Collection("collection1")]
        public class CanAddMoreCarsMethods
        {
            private CarService Service;
            public CanAddMoreCarsMethods(SharedService s)
            {
                Service = s.CarService;
            }

            [Fact]
            public void MemberCanAddNotMoreThanTwoCar()
            {
                //var db = new FakeCarDb();
                //var s = new CarService(db);

                var memberId = Guid.NewGuid();

                Service.CanAddMoreCars(memberId).ShouldBeTrue();

                Service.AddCar(new Car(), memberId);  // 1st car
                Service.CanAddMoreCars(memberId).ShouldBeTrue();

                Service.AddCar(new Car(), memberId);  // 2nd car
                Service.CanAddMoreCars(memberId).ShouldBeFalse();
            }
        }
    }
}
