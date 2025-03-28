using mvcproj.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.View_Models
{
    public class RoomUpdateViewModel
    {
        public int RoomID { get; set; }

        public string? Image { get; set; }
        public string? Status { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public RoomType? RoomType { get; set; }

        public string? HotelName { get; set; }

        [Display(Name = "Room Type")]
        public int TypeID { get; set; }
        public List<RoomType>? RoomTypeList { get; set; } = new List<RoomType>();
    }
}
