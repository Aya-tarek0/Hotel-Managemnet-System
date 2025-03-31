using mvcproj.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.View_Models
{
    public class ShowRoomDetailsWithCommentsViewModel
    {

        public int RoomID { get; set; }
        public int HotelID { get; set; }
        public int TypeID { get; set; }
        public string? ImageUrl { set; get; }
        public string? RoomStatus { get; set; }
        public string? RoomTypeName { get; set; }
        public string? Description { get; set; }
        public int? PricePerNight { get; set; }
        public int? Capacity { get; set; }
        public string? HotelName { get; set; }

        public List<CommentsWithRoomIDViewModel> Comments { get; set; }

    }
}
