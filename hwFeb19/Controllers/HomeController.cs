using hwFeb19.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hwFeb19.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Cars; Integrated Security=true;";

        public IActionResult Index(string sort)
        {
            var manager = new CarManager(_connectionString);
            var model = new CarViewModel { Cars = manager.GetCars(sort), UpDown = sort == "asc" };
            return View(model);
        }

        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            var manager = new CarManager(_connectionString);
            manager.InsertCar(car);
            return Redirect("/home/index");
        }
    }
}