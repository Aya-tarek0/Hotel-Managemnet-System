using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.View_Models;

namespace mvcproj.Reporisatory
{
    public class CommentReporisatory : IGenericReporisatory<Comment>, ICommentReporisatory
    {
        Reservecotexet _context;
        public CommentReporisatory(Reservecotexet context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var comment = GetById(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
        }

        public List<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.CommentID == id);
        }

        public List<CommentsWithRoomIDViewModel> GetCommentsByRoomId(int id)
        {
            return _context.Comments
                .Where(c => c.RoomID == id)
                .OrderByDescending(c => c.CommentDate)
                .Include(c=>c.Guest)
                .Select(c=> new CommentsWithRoomIDViewModel
                {
                     CommentText =c.CommentText,
                     CreatedAt =c.CommentDate,
                     RoomID =c.RoomID,
                     GuestName=c.Guest!=null?c.Guest.Name : "Anonymous",
                     GuestEmail =c.Guest!=null?c.Guest.Email: "No Email",
                })
                .ToList();
        }

        public void Insert(Comment obj)
        {
            _context.Comments.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        { 
            await _context.SaveChangesAsync();
        
        }

        public void Update(Comment obj)
        {
            _context.Update(obj);
        }

    
    }
}
