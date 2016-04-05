using CarFuel.Model;
using CarFuel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CarFuel.DataAccess;

namespace CarFuel.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        //private static List<Car> cars = new List<Car>();
        private ICarDb db;
        private CarService carService ;

        public CarsController()
        {
            db = new CarDb();
            carService = new CarService(db);
        }

        public ActionResult Index()
        {
            var userId = new Guid(User.Identity.GetUserId());
            IEnumerable<Car> cars = carService.GetCarsByMember(userId);
            return View(cars);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Car item)
        {
            //cars.Add(item);
            var userId = new Guid(User.Identity.GetUserId());
            carService.AddCar(item, userId);
            return RedirectToAction("Index");
        }
    }
}