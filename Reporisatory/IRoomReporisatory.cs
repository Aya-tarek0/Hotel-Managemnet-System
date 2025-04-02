using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface IRoomReporisatory:IGenericReporisatory<Room>
    {
        //public IEnumerable<Room> GetRoomStatus();
        Room GetRoomDetailsById(int id);
        List<Room> CheckAvailability(DateTime checkIn, DateTime checkOut, int roomTypeId, int NumberOfGuests);

    }
}
