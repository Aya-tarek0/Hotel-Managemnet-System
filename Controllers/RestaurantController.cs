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
        if (ImageFile == null)
        {
            ModelState.Remove("ImageFile"); // إزالة التحقق من الحقل
        }

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
                restaurantRepository.Update(restaurant);
                restaurantRepository.Save();
                return RedirectToAction("ShowAllFood", "Restaurant");
            }
        }

        return View("UpdateDish", restaurantUpdated);
    }


}
