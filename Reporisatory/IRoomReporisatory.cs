using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface IRoomReporisatory:IGenericReporisatory<Room>
    {
        //public IEnumerable<Room> GetRoomStatus();
        Room GetRoomDetailsById(int id);
        List<Room> CheckAvailability( int roomTypeId, int NumberOfGuests);

    }
}
//