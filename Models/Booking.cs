using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public Guest? Guest { get; set; }

        [ForeignKey("Room")]
        public int RoomNumber { get; set; }

       
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int TotalPrice { get; set; }
        public bool IsDeleted { get; set; } // Soft delete flag

        public Room? Room { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
