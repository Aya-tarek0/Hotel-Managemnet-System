using mvcproj.Models;

namespace mvcproj.View_Models
{
    public class HomeViewModel
    {
        public List<RoomType> RoomTypes { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
