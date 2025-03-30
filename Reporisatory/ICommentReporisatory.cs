using Microsoft.EntityFrameworkCore;
using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface ICommentReporisatory:IGenericReporisatory<Comment>
    {
        List<Comment> GetCommentsByRoomId(int RoomID);


        Task SaveAsync(); 
       
    }
}
