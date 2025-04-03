
using Microsoft.AspNetCore.Mvc;
using mvcproj.Reporisatory;

namespace mvcproj.Controllers
{
    public class RoomTypeController : Controller
    {
        IRoomTypeReporisatory roomTypeRepo;
        public RoomTypeController (IRoomTypeReporisatory roomTypeRepo)
        {
            this.roomTypeRepo = roomTypeRepo;
        }
        public IActionResult Index()
        {
            var roomTypes = roomTypeRepo.GetAll(); 
            return View("~/Views/Home/Index.cshtml", roomTypes);
        }
    }
}
