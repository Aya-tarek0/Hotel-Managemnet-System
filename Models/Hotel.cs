using System.ComponentModel.DataAnnotations;

namespace mvcproj.Models
{
    public class Hotel
    {
        
        public int HotelID { get; set; }

       
        public string? Name { get; set; }

        public string? Address { get; set; }

       
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public int? Stars { get; set; }

        public string? image { set; get; }

        public DateTime CheckinTime { get; set; }

        public DateTime CheckoutTime { get; set; }

        public List<Room> ?Rooms { get; set; }
        public List<Staff> ?Staffs { get; set; }
    }
}
