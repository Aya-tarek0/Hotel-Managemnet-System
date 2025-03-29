using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using mvcproj.Reporisatory;
using mvcproj.View_Models;

namespace mvcproj.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        #region GetAll
        public IActionResult Index()
        {

            List<Booking> bookingList = bookingRepository.GetAll();
            BookingViewModel bookingViewModel = new BookingViewModel();
            foreach (Booking booking in bookingList)
            {
                bookingViewModel.BookingID = booking.BookingID;
                bookingViewModel.UserId = booking.UserId;
                bookingViewModel.Guest = booking.Guest.Name;
                bookingViewModel.RoomNumber = booking.RoomNumber;
                bookingViewModel.CheckinDate = booking.CheckinDate;
                bookingViewModel.CheckoutDate = booking.CheckoutDate;
                bookingViewModel.TotalPrice = booking.TotalPrice;
                bookingViewModel.Room = booking.Room.Status;
            }

            return View("Index", bookingViewModel);
        }
        #endregion

        #region AddBooking


        public IActionResult Add()
        {
            return View("Add");
        }
        public IActionResult Add(BookingViewModel bookingVM)
        {
            Booking booking = new Booking()
            {
               
                UserId = bookingVM.UserId,
                RoomNumber = bookingVM.RoomNumber??0,
                CheckinDate = bookingVM.CheckinDate??DateTime.Now,
                CheckoutDate = bookingVM.CheckoutDate??DateTime.Now,
                TotalPrice = bookingVM.TotalPrice ?? 0
            };

            bookingRepository.Insert(booking);
            return View();
        }

        #endregion
    }
}
