using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.Models
{
    public class Guest
    {

        //[Key]
        [Key]  
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
     
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
