using mvcproj.Models;

namespace mvcproj.View_Models
{
    public class BookingViewModel
    {
        public int BookingID { get; set; }

        public string UserId { get; set; }
        //[ForeignKey("UserId")]
        public string Guest { get; set; }

        //[ForeignKey("Room")]
        public int? RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int? TotalPrice { get; set; }

        public string? Room { get; set; }
    }
}
