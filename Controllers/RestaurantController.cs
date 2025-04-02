using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using mvcproj.Reporisatory;

public class RestaurantController : Controller
{
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IWebHostEnvironment webHostEnvironment;

    public RestaurantController(IRestaurantRepository restaurantRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.restaurantRepository = restaurantRepository;
        this.webHostEnvironment = webHostEnvironment;
    }

    public IActionResult ShowAllFood()
    {
        var food = restaurantRepository.GetAll();
        return View("Admin/ShowAllFood", food);
    }

    [HttpPost]
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

            restaurantRepository.Insert(restaurant);
            restaurantRepository.Save();
            return RedirectToAction("ShowAllFood");
        }

        return View("AddDish", restaurant); 
    }

    public IActionResult AddDish()
    {
        return View();
    }
    public IActionResult UpdateDish(int id)
    {
        // Get the existing restaurant from the repository
        Restaurant restaurant = restaurantRepository.GetById(id);

        // If the restaurant does not exist, redirect to an error page or show a not found view
        if (restaurant == null)
        {
            return NotFound();
        }

        // Return the current restaurant data to the view for editing
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
                restaurantRepository.Update(restaurant);
                restaurantRepository.Save();
                return RedirectToAction("ShowAllFood", "Restaurant");
            }
        }

        return View("UpdateDish", restaurantUpdated);
    }

    public async Task<IActionResult> Delete(int id)
    {
        Restaurant res = restaurantRepository.GetById(id);

        if(res != null)
        {
            restaurantRepository.Delete(id);
            restaurantRepository.Save();
            return RedirectToAction("ShowAllFood");
        }

        return NotFound("not found");
    }
}
