using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using mvcproj.Reporisatory;

namespace mvcproj.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IWebHostEnvironment webHostEnvironment;


        public RestaurantController(IRestaurantRepository restaurantRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.restaurantRepository = restaurantRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

//<<<<<<< HEAD
        //    return View("AddDish", restaurant); 
        //}
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

            if (userType == "Admin")
            {
                return View("Admin/ShowAllFood", food);
            }
            else
            {
                return PartialView("_RestaurantMenuPartial", food);
            }
            //return View("Admin/ShowAllFood", food);
        }
        public IActionResult ViewAllMenu()
        {
            var food = restaurantRepository.GetAll();
            return View("User/ViewAllMenu", food);
        }

        public async Task<IActionResult> SaveDish(Restaurant restaurant, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    restaurant.ImageUrl = "/uploads/" + uniqueFileName;
                }
                List<Restaurant> restmenu = restaurantRepository.GetAll();

                restaurantRepository.Insert(restaurant);
                restaurantRepository.Save();
                return View("Admin/ShowAllFood",restmenu);
            }

            return View("AddDish", restaurant);
        }

        [Authorize(Roles = "Admin")]

        public IActionResult AddDish()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        public IActionResult UpdateDish(int id)
        {
            Restaurant restaurant = restaurantRepository.GetById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View("UpdateDish", restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdateDish(Restaurant restaurantUpdated, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                Restaurant restaurant = restaurantRepository.GetById(restaurantUpdated.RestaurantId);

                if (restaurant != null)
                {
                    restaurant.Name = restaurantUpdated.Name;
                    restaurant.Price = restaurantUpdated.Price;
                    restaurant.Description = restaurantUpdated.Description;

                    if (ImageFile != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        restaurant.ImageUrl = "/uploads/" + uniqueFileName;
                    }
                    else
                    {
                        restaurant.ImageUrl = restaurant.ImageUrl ?? restaurantUpdated.ImageUrl;
                    }
                    List<Restaurant> restmenu= restaurantRepository.GetAll();
                    
                    restaurantRepository.Update(restaurant);
                    restaurantRepository.Save();
                    return View /*RedirectToAction*/("Admin/ShowAllFood", restmenu/*, "Restaurant"*/);
                }
            }

            return View("UpdateDish", restaurantUpdated);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var restaurant = restaurantRepository.GetById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View("Admin/Details",restaurant); 
        }




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Restaurant res = restaurantRepository.GetById(id);

            if (res != null)
            {
                restaurantRepository.Delete(id);
               

                restaurantRepository.Save();
                List<Restaurant> restmenu = restaurantRepository.GetAll();

                return View("Admin/ShowAllFood",restmenu);
            }

            return NotFound("not found");
        }


    }
}
//<<<<<<< HEAD
//}
//=======

//    public async Task<IActionResult> Delete(int id)
//    {
//        Restaurant res = restaurantRepository.GetById(id);

//        if(res != null)
//        {
//            restaurantRepository.Delete(id);
//            restaurantRepository.Save();
//            return RedirectToAction("ShowAllFood");
//        }

//        return NotFound("not found");
//    }
//}
//>>>>>>> 861567ed359e85badf9ecc793d1fe03b30ccb320
