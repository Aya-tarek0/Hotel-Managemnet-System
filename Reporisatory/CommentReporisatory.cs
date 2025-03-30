using mvcproj.Models;

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

        public List<Comment> GetCommentsByRoomId(int id)
        {
            return _context.Comments
                .Where(c => c.RoomID == id)
                .ToList();
        }

        public void Insert(Comment obj)
        {
            _context.Add(obj);
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
