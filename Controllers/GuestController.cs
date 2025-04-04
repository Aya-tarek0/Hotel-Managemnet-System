using Microsoft.AspNetCore.Mvc;

namespace mvcproj.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }
    }
}
