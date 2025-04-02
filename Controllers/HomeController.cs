using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using mvcproj.Reporisatory;
using System.Diagnostics;

namespace mvcproj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestaurantRepository restaurantRepository;

        public HomeController(ILogger<HomeController> logger,IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            this.restaurantRepository = restaurantRepository;
        }

        public IActionResult Index()
        {

            var food = restaurantRepository.GetAll();
            return View(food);
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