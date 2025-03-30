using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> user;
        public BookingController(IBookingRepository bookingRepository,IRoomTypeReporisatory roomTypeReporisatory,UserManager<ApplicationUser> userManager)
        {
           user = userManager;

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

        #region details
        public IActionResult details(int id)
        {
            Booking book = bookingRepository.GetdetailsById(id);
            if (book == null)
            {
                return NotFound();
            }
            BookingViewModel showbookmodel = new BookingViewModel
            {
                BookingID = book.BookingID,
                UserId = book.UserId,
                Guest = book.Guest?.Name,
                RoomNumber = book.RoomNumber,
                CheckinDate = book.CheckinDate,
                CheckoutDate = book.CheckoutDate,
                TotalPrice = book.TotalPrice,
            };
            return View("details", showbookmodel);

        }
        #endregion

        #region AddBooking

        [Authorize]
        public IActionResult Add()
        {
            ViewBag.RoomTypes = new SelectList(roomTypeReporisatory.GetAll(), "RoomTypeId", "Name");
            return View("Add");
        }
        public IActionResult SaveAdd(BookingViewModel bookingVM)
        {

            RoomType roomType = roomTypeReporisatory.GetById(bookingVM.RoomType.RoomTypeId);
            

            int noOfDays = (bookingVM.CheckoutDate.GetValueOrDefault() - bookingVM.CheckinDate.GetValueOrDefault()).Days;
            var userId = user.GetUserId(User);

            Booking booking = new Booking()
            {
                UserId = userId,

               
                RoomNumber = bookingVM.RoomNumber??0,
                CheckinDate = bookingVM.CheckinDate??DateTime.Now,
                CheckoutDate = bookingVM.CheckoutDate??DateTime.Now,
                TotalPrice = bookingRepository.CalcTotalPrice( noOfDays,roomType?.PricePerNight.GetValueOrDefault() ?? 0)
                
            };
          
            bookingRepository.Insert(booking);
            //return View("Add");
            return RedirectToAction("details", new { id = booking.BookingID });
        }

        #endregion
    }
}
