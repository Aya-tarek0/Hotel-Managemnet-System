using Microsoft.AspNetCore.Identity;

namespace mvcproj.Models
{
    public class ApplicationUser: IdentityUser
    {
        public Guest GuestProfile { get; set; } 
        public Staff StaffProfile { get; set; }

        public string? Address { set; get; }
    }
}
