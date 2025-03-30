using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface IBookingRepository :IGenericReporisatory<Booking>
    {
        int CalcTotalPrice(int DaysNo , int PricePerDay);
    }
}
