using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;

        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public Room? Room { get; set; }

        [ForeignKey("Guest")]
        public string? GuestID { get; set; }
        public Guest? Guest { get; set; }

    }
}
