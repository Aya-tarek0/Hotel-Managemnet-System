using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.Reporisatory;
using mvcproj.View_Models;

namespace mvcproj.Controllers
{
    //Room/Index

        //Room/Index
        public class RoomController : Controller
        {

            #region Injection Database And Reporisatory
            IRoomReporisatory roomRepo;
            IRoomTypeReporisatory roomTypeRepo;
            ICommentReporisatory commentRepo; 
            private readonly IWebHostEnvironment webHostEnvironment;

            public RoomController(IRoomReporisatory roomRepo, IRoomTypeReporisatory roomTypeRepo, IWebHostEnvironment webHostEnvironment, ICommentReporisatory commentRepo)
            {
                this.roomRepo = roomRepo;
                this.roomTypeRepo = roomTypeRepo;
                this.webHostEnvironment = webHostEnvironment;
                this.commentRepo = commentRepo;
            }
        #endregion

            #region Show All Rooms
        /* public IActionResult Index()
         {
             List<Room> roomList = roomRepo.GetAll();
             return View("Index", roomList);
         }*/
        public IActionResult Index(int page = 1)
        {
            if (roomRepo == null)
            {
                return View("Error");
            }

            var rooms = roomRepo.GetAll();

            List<ShowRoomDetailsWithCommentsViewModel> roomList = rooms
                .Select(e => new ShowRoomDetailsWithCommentsViewModel
                {
                    RoomID = e.RoomID,
                    ImageUrl = e.image,
                    RoomTypeName = e.RoomType?.Name,
                    TypeID=e.TypeID,
                    HotelID=e.HotelID,

                    PricePerNight = e.RoomType.PricePerNight,
                    RoomStatus = e.Status
                }).ToList();

            int pageSize = 4;
            int totalRooms = roomList.Count;
            int totalPages = (int)Math.Ceiling((double)totalRooms / pageSize);

            var paginatedRooms = roomList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            string userType = GetUserType();

            if (userType == "Admin")
            {
                return View("Index", paginatedRooms);
            }
            else
            {
                return View("_AllRoomsUser", paginatedRooms);
            }
        }



        private string GetUserType()
        {
            if (User.IsInRole("Admin"))
                return "Admin"; 
            if (User.IsInRole("Guest"))
                return "Guest";

            return "User";
        }



        #endregion

            #region Add New Room

        public IActionResult AddRoom()
            {
                var roomTypes = roomTypeRepo.GetRoomType();
                ViewBag.RoomTypes = new SelectList(roomTypes, "TypeID", "Name");
                return View("AddRoom");
            }
            [HttpPost]
            public async Task<IActionResult> SaveRoom(Room room)
            {
                if (ModelState.IsValid)
                {
                    if (room.ImageFile != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(room.ImageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await room.ImageFile.CopyToAsync(fileStream);
                        }

                        room.image = "/uploads/" + uniqueFileName;
                    }

                    roomRepo.Insert(room);
                    return RedirectToAction("Index");
                }

                var roomTypes = roomTypeRepo.GetRoomType();
                ViewBag.RoomTypes = new SelectList(roomTypes, "TypeID", "TypeName", room.TypeID);
                return View("AddRoom"); // 🔹 إذا كان هناك خطأ، يعيد المستخدم لنفس الصفحة مع الأخطاء
            }

            #endregion

            #region Edit Room Information 

            public IActionResult UpdatRoomInformation(int id)
            {
                var room = roomRepo.GetRoomDetailsById(id);

                if (room == null)
                {
                    return NotFound();
                }
                RoomUpdateViewModel roomUpdate = new RoomUpdateViewModel
                {
                    RoomID = room.RoomID,
                    Image = room.image,
                    Status = room.Status,
                    ImageFile = room.ImageFile,
                    TypeID = room.TypeID,
                    HotelName = room.Hotel?.Name,
                    RoomTypeList = roomTypeRepo.GetAll()
                };
                return View("UpdatRoomInformation", roomUpdate);
            }

            public IActionResult SaveUpdateRoom(RoomUpdateViewModel roomUpdate)
            {
                if (ModelState.IsValid)
                {
                    Room room = roomRepo.GetById(roomUpdate.RoomID);
                    if (room == null)
                    {
                        return NotFound();
                    }

                    room.image = roomUpdate.Image;
                    room.Status = roomUpdate.Status;
                    room.ImageFile = roomUpdate.ImageFile;
                    room.TypeID = roomUpdate.TypeID;

                    roomRepo.Update(room);
                    roomRepo.Save();

                    return RedirectToAction("Index", "Room");
                }

                roomUpdate.RoomTypeList = roomTypeRepo.GetAll();
                return View("UpdatRoomInformation", roomUpdate);
            }

            #endregion

            #region Show Room Details
            public IActionResult ShowRoomDetails(int id)
            {
                Room room = roomRepo.GetRoomDetailsById(id);
                if (room == null)
                {
                    return NotFound();
                }
            var comments = commentRepo.GetCommentsByRoomId(id);

            ShowRoomDetailsWithCommentsViewModel showRoomModel = new ShowRoomDetailsWithCommentsViewModel
                {
                    RoomID = room.RoomID,
                    HotelID = room.HotelID,
                    HotelName = room.Hotel?.Name,
                    TypeID = room.TypeID,
                    ImageUrl = room.image,
                    RoomStatus = room.Status,
                    RoomTypeName = room.RoomType?.Name,
                    Description = room.RoomType?.Description,
                    PricePerNight = room.RoomType?.PricePerNight,
                    Capacity = room.RoomType?.Capacity,
                    Comments=comments,

                };
            //return View("ShowRoomDetails", showRoomModel);// show details for Admin 
            return View("~/Views/Room/User/ShowRoomDetailsUser.cshtml", showRoomModel); //show details for user 

        }

        #endregion

            #region Delete

        public IActionResult Delete(int id)
        {
            Room room = roomRepo.GetRoomDetailsById(id);
            if (room != null)
            {
                ShowRoomDetailsWithCommentsViewModel RoomViewModel = new ShowRoomDetailsWithCommentsViewModel()
                {
                    RoomID = room.RoomID,
                    HotelID = room.HotelID,
                    HotelName = room.Hotel?.Name,
                    TypeID = room.TypeID,
                    ImageUrl = room.image,
                    RoomStatus = room.Status,
                    RoomTypeName = room.RoomType?.Name,
                    Description = room.RoomType?.Description,
                    PricePerNight = room.RoomType?.PricePerNight,
                    Capacity = room.RoomType?.Capacity
                };
                return View("Delete", RoomViewModel);
            }
            return NotFound("Room doesn't Exist");
        }

        public IActionResult SaveDelete(int id)
        {
            Room room = roomRepo.GetById(id);
            if (room != null)
            {
                room.IsDeleted = true;
                roomRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Room doesn't Exist");
            }
        }

        #endregion

            #region chack availability
       
            public IActionResult GetAvailableRooms(DateTime checkIn,DateTime checkOut,int roomTypeId, int capacity)
            {
                var availableRooms = roomRepo.CheckAvailability(checkIn, checkOut, roomTypeId, capacity);
            

                var roomViewModels = availableRooms.Select(room => new ShowRoomDetailsWithCommentsViewModel
                {
                    RoomID = room.RoomID,
                    ImageUrl = room.image,
                    RoomTypeName = room.RoomType?.Name ,
                    TypeID = room.TypeID,
                    HotelID = room.HotelID,
                    PricePerNight = room.RoomType?.PricePerNight ,
                    RoomStatus = room.Status
                }).ToList();

                return roomViewModels.Any()
                ? View("_AllRoomsUser", roomViewModels)
                : Content("no available rooms");
            }
            #endregion

    }
}

