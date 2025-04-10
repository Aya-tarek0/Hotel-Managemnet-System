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
        UserManager<ApplicationUser> _userManager;
        public CommentController(ICommentReporisatory commentRepo , IHubContext<CommentsHub> hubContext,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized(); 
                }

                var comment = new Comment
                {
                    CommentText = CommentModel.CommentText,
                    RoomID = CommentModel.RoomID,
                    CommentDate = CommentModel.CreatedAt,
                    GuestID = user.Id
                };

                commentReporisatory.Insert(comment);
                await commentReporisatory.SaveAsync();

                var comments = commentReporisatory.GetCommentsByRoomId(CommentModel.RoomID);


                await _hubContext.Clients.All.SendAsync("ReceiveComment",
                    user.UserName, user.Email, CommentModel.CreatedAt, CommentModel.CommentText);

                return Ok(new { message = "Comment added successfully"  });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when saving comment: {ex.Message}");
            }
        }

    }
}
