using Microsoft.AspNetCore.Mvc;

namespace mvcproj.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
