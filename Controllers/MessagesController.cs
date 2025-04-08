using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.Reporisatory;

namespace mvcproj.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService messageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(IMessageService ms, UserManager<ApplicationUser> userManager)
        {
            messageService = ms;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Guest"))
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var adminUser = adminUsers.FirstOrDefault();

                if (adminUser != null)
                {
                    return RedirectToAction("Chat", new { selecteduserid = adminUser.Id });
                }
            }

            var users = await messageService.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Chat(string selecteduserid)
        {
            if (User.IsInRole("Guest"))
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var adminUser = adminUsers.FirstOrDefault();

                if (adminUser != null)
                {
                    selecteduserid = adminUser.Id;
                }
            }

            var chatViewModel = await messageService.GetMessages(selecteduserid);
            return View(chatViewModel);
        }
    }
    }
