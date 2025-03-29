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
    public class RoomController : Controller
    {
        
        #region Injection Database And Reporisatory
        IRoomReporisatory roomRepo;
        IRoomTypeReporisatory roomTypeRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RoomController(IRoomReporisatory roomRepo, IRoomTypeReporisatory roomTypeRepo, IWebHostEnvironment webHostEnvironment)
        {
            this.roomRepo = roomRepo;
            this.roomTypeRepo = roomTypeRepo;
            this.webHostEnvironment = webHostEnvironment;
        } 
        #endregion

        #region Show All Rooms
        public IActionResult Index()
        {
            List<Room> roomList = roomRepo.GetAll();
            return View("Index", roomList);
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
            if(room== null)
            {
                return NotFound();
            }
            ShowRoomDetailsViewModel showRoomModel = new ShowRoomDetailsViewModel
            {
                RoomID = room.RoomID,
                HotelID = room.HotelID,
                HotelName=room.Hotel?.Name,
                TypeID = room.TypeID,
                ImageUrl = room.image,
                RoomStatus = room.Status,
                RoomTypeName = room.RoomType?.Name,
                Description = room.RoomType?.Description,
                PricePerNight = room.RoomType?.PricePerNight,
                Capacity = room.RoomType?.Capacity
            };
            return View("ShowRoomDetails", showRoomModel);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            Room room = roomRepo.GetRoomDetailsById(id);
            if (room != null)
            {
                ShowRoomDetailsViewModel RoomViewModel = new ShowRoomDetailsViewModel()
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

    }
}
