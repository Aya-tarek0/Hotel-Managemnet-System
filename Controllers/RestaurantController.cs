using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using mvcproj.Reporisatory;

namespace mvcproj.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }
        public IActionResult ShowAllFood()
        {
            var food = restaurantRepository.GetAll();
            return View("Admin/ShowAllFood", food);
        }
    }
}
