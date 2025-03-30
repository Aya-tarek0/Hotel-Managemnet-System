using mvcproj.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.View_Models
{
    public class CommentsWithRoomIDViewModel
    {

        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int RoomID { get; set; }
        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }

    }
}
