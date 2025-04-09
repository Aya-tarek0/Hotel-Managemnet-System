using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface IRoomTypeReporisatory:IGenericReporisatory<RoomType>
    {
        public IEnumerable<RoomType> GetRoomType();
        public RoomType GetRoomByStatus(string status);
    }
}
