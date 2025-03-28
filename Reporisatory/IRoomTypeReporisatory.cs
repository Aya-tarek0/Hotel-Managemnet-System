using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface IRoomTypeReporisatory:IGenericReporisatory<RoomType>
    {
        public IEnumerable<RoomType> GetRoomType();
    }
}
