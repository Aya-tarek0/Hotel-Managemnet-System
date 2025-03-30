using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.View_Models;

namespace mvcproj.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Reservecotexet _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , RoleManager<IdentityRole> _roleManager , Reservecotexet context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            roleManager = _roleManager;
            _context = context;
        }

        #region Register Guest

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Address= model.Address
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await roleManager.RoleExistsAsync("Guest"))
            {
                await roleManager.CreateAsync(new IdentityRole("Guest"));
            }

            await userManager.AddToRoleAsync(user, "Guest");

            var guest = new Guest
            {
                UserId = user.Id, 
                Name = model.UserName,
                Phone = model.Phone,
                Email = model.Email,
                Address =model.Address
                
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index","Room");
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appFromDb = await userManager.FindByNameAsync(userFromRequest.UserName);

                if (appFromDb != null)
                {
                    bool isPasswordCorrect = await userManager.CheckPasswordAsync(appFromDb, userFromRequest.Password);

                    if (isPasswordCorrect)
                    {
                        List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, appFromDb.UserName),
                    new Claim(ClaimTypes.Email, appFromDb.Email ?? "No Email")
                };

                        var roles = await userManager.GetRolesAsync(appFromDb);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        await signInManager.SignInWithClaimsAsync(appFromDb, userFromRequest.RememberMe, claims);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Admin"); 
                        }
                        else
                        {
                            return RedirectToAction("Index", "Guest"); 
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View("Login", userFromRequest);
        }
        #endregion

        #region Register Admin
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await userManager.AddToRoleAsync(user, "Admin");

            var staff = new Staff
            {
                UserId = user.Id,
                Name = model.UserName,
                Phone = model.Phone,
                Email = model.Email,

            };

            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            await signInManager.SignInAsync(user, isPersistent: false);

            TempData["SuccessMessage"] = "Admin registered successfully!";
            return RedirectToAction("RegisterAdmin");
        }
        #endregion
        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Room");
        }
        #endregion
    }
}
