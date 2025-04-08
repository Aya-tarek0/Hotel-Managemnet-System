using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using mvcproj.Reporisatory;
using mvcproj.View_Models;
using System.Diagnostics;

namespace mvcproj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IRoomTypeReporisatory roomTypeReporisatory;

        public HomeController(ILogger<HomeController> logger,IRestaurantRepository restaurantRepository, IRoomTypeReporisatory roomTypeReporisatory)
        {
            _logger = logger;
            this.restaurantRepository = restaurantRepository;
            this.roomTypeReporisatory = roomTypeReporisatory;
        }

        public IActionResult Index()
        {

            var viewModel = new HomeViewModel
            {
                Restaurants = restaurantRepository.GetAll(),
                RoomTypes = roomTypeReporisatory.GetAll()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}