using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface IBookingRepository :IGenericReporisatory<Booking>
    {
        int CalcTotalPrice(int DaysNo , int PricePerDay);
        public Booking GetdetailsById(int id);

        public List<Booking> GetBookingsByUserId(string userId);

        public List<Booking> GetBookingsByRoomId(int roomId);

        // public int GetRoomId (int id);
    }
}
