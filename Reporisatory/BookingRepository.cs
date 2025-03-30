using Microsoft.EntityFrameworkCore;
using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public class BookingRepository : IBookingRepository , IGenericReporisatory<Booking>
    {
        Reservecotexet context;
        public BookingRepository(Reservecotexet context)
        {
            this.context = context;
        }

        public int CalcTotalPrice(int DaysNo, int PricePerDay)
        {
            return DaysNo * PricePerDay;
        }

        public void Delete(int id)
        {
            Booking book = GetById(id);
            context.Remove(book);
        }

        public List<Booking> GetAll()
        {

            List<Booking> allbooking = context.Bookings
                .Include(b => b.Guest).
                Include(r => r.Room).
                Where(b=>!b.IsDeleted).ToList();

            return allbooking;
        }

        public Booking GetById(int id)
        {
            Booking book = context.Bookings.FirstOrDefault(r => r.BookingID == id);
            return book;
        }

        public void Insert(Booking obj)
        {
            context.Add(obj);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Booking obj)
        {
            context.Update(obj);
        }
    }
}
   
