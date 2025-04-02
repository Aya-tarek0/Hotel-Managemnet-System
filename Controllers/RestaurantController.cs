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
}
