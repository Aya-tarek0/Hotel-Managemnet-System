using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace mvcproj.Models
{
    public class ApplicationUser: IdentityUser
    {
        public Guest GuestProfile { get; set; } 
        public Staff StaffProfile { get; set; }

        public string? Address { set; get; }

        [InverseProperty(nameof(Message.Sender))]
        public ICollection<Message> SendMessages { set; get; }

        [InverseProperty(nameof(Message.Receiver))]

        public ICollection<Message> ReceivedMessages{ set; get; }

    }
}
