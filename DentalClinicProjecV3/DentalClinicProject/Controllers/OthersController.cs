using Microsoft.AspNetCore.Mvc;

namespace DentalClinicProject.Controllers
{
    public class OthersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
