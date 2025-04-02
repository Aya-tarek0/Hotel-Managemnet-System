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
        private string GetUserType()
        {
            if (User.IsInRole("Admin"))
                return "Admin";
            if (User.IsInRole("Guest"))
                return "Guest";

            return "User";
        }
        public IActionResult ShowAllFood()
        {
            var food = restaurantRepository.GetAll();
            string userType = GetUserType();

            //if (userType == "Admin")
            //{
            //    return View("Admin/ShowAllFood", food);
            //}
            //else
            //{
            //    return PartialView("_RestaurantMenuPartial", food);
            //}
            return View("Admin/ShowAllFood", food);

        }
        public IActionResult ViewAllMenu()
        {
            var food = restaurantRepository.GetAll();
            return View("User/ViewAllMenu", food);
        }
    }
}
