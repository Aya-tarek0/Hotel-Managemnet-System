using System.ComponentModel.DataAnnotations.Schema;

///
/////
namespace mvcproj.Models 
{
    public class Room
    {
        public int RoomID { get; set; }

        [ForeignKey("Hotel")]
        public int HotelID { get; set; }

        [ForeignKey("RoomType")]
        public int TypeID { get; set; }

        public string? image { set; get; }
        
        public string? Status { get; set; }

        public Hotel ?Hotel { get; set; }
        public bool IsDeleted { get; set; } // Soft delete flag
        public RoomType? RoomType { get; set; }
        [NotMapped] 
        public IFormFile? ImageFile { get; set; }
    }
}
