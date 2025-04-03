using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using mvcproj.Hubs;
using mvcproj.Models;
using mvcproj.Reporisatory;
using mvcproj.View_Models;
using System.Threading.Tasks;

namespace mvcproj.Controllers
{
    public class CommentController : Controller
    {
        ICommentReporisatory commentReporisatory;
        IHubContext<CommentsHub> _hubContext;
        UserManager<ApplicationUser> user;
        public CommentController(ICommentReporisatory commentRepo , IHubContext<CommentsHub> hubContext,UserManager<ApplicationUser> userr)
        {
            user = userr;
            commentReporisatory = commentRepo;
            _hubContext = hubContext;
        }
        public IActionResult Index(int id)
        {
            var comments = commentReporisatory.GetCommentsByRoomId(id);
            return View("~/Views/Room/User/_CommentsPartial.cshtml", comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentsWithRoomIDViewModel CommentModel)
        {
            if (!ModelState.IsValid)
            {
                return View(CommentModel);
            }
            try
            {

                var comment = new Comment
                {
                    CommentText = CommentModel.CommentText,
                    RoomID = CommentModel.RoomID,
                    CommentDate = CommentModel.CreatedAt,
                    GuestID = user.GetUserId(User)
                };
                commentReporisatory.Insert(comment);

                //return RedirectToAction("ShowRoomDetails", "Room", new { id = CommentModel.RoomID });

                await commentReporisatory.SaveAsync();
                var name = user.GetUserName(User);
                _hubContext.Clients.All.SendAsync("ReceiveComment"
                    ,name,CommentModel.CreatedAt, CommentModel.CommentText);

                return Ok(new { message = "Comment added successfully" });
            }
            catch
            {
                return StatusCode(500, "Error when save comment");
            }
        }
    }
}
