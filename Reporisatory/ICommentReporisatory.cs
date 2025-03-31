using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.View_Models;

namespace mvcproj.Reporisatory
{
    public interface ICommentReporisatory:IGenericReporisatory<Comment>
    {
        List<CommentsWithRoomIDViewModel> GetCommentsByRoomId(int RoomID);

        Task SaveAsync(); 
       
    }
}
