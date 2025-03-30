using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcproj.Models;
using mvcproj.Reporisatory;
using mvcproj.View_Models;

namespace mvcproj.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IRoomTypeReporisatory roomTypeReporisatory;

        public BookingController(IBookingRepository bookingRepository,IRoomTypeReporisatory roomTypeReporisatory)
        {
            this.bookingRepository = bookingRepository;
            this.roomTypeReporisatory = roomTypeReporisatory;
        }

        #region GetAll
        public IActionResult Index()
        {

            List<Booking> bookingList = bookingRepository.GetAll();
            List<BookingViewModel> bookingViewModelList = new List<BookingViewModel>();
            foreach (Booking booking in bookingList)
            {

                bookingViewModelList.Add(new BookingViewModel()
                {
                    BookingID = booking.BookingID,
                     UserId = booking.UserId,
                  Guest = booking.Guest.Name,
                   RoomNumber = booking.RoomNumber,
                    CheckinDate = booking.CheckinDate,
                    CheckoutDate = booking.CheckoutDate,
                   TotalPrice = booking.TotalPrice,
                    Room = booking.Room.Status
                });

              
            }

            return View("Index", bookingViewModelList);
        }


        //public IActionResult getallres()
        //{
        //    List<Booking> bookinglist = bookingRepository.GetAll();
        //    return View("index2", bookinglist);

        //}
        #endregion



        #region AddBooking


        public IActionResult Add()
        {
            ViewBag.RoomTypes = new SelectList(roomTypeReporisatory.GetAll(), "RoomTypeId", "Name");
            return View("Add");
        }
        public IActionResult SaveAdd(BookingViewModel bookingVM)
        {

            RoomType roomType = roomTypeReporisatory.GetById(bookingVM.RoomType.RoomTypeId);
            

            int noOfDays = (bookingVM.CheckoutDate.GetValueOrDefault() - bookingVM.CheckinDate.GetValueOrDefault()).Days;
            Booking booking = new Booking()
            {
               
                UserId = bookingVM.UserId,
                RoomNumber = bookingVM.RoomNumber??0,
                CheckinDate = bookingVM.CheckinDate??DateTime.Now,
                CheckoutDate = bookingVM.CheckoutDate??DateTime.Now,
                TotalPrice = bookingRepository.CalcTotalPrice( noOfDays,roomType?.PricePerNight.GetValueOrDefault() ?? 0)
                
            };
          
            bookingRepository.Insert(booking);
            return View("Add");
        }

        #endregion
    }
}
