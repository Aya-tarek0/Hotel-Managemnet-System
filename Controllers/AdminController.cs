using Microsoft.AspNetCore.Mvc;

namespace mvcproj.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("SideBarView","Room");
        }
    }
}
