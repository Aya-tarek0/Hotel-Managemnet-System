using Microsoft.AspNetCore.Http.HttpResults;
using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public class RoomTypeReporisatory:IRoomTypeReporisatory
    {
        Reservecotexet context;
        public RoomTypeReporisatory(Reservecotexet context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            RoomType roomType = GetById(id);
            if (roomType != null)  
            {
                context.Remove(roomType);
                Save();
            }
        }

        public List<RoomType> GetAll()
        {
            List<RoomType> roomTypes = context.RoomTypes.ToList();
            return roomTypes;
        }

        public RoomType GetById(int id)
        {
            RoomType roomType = context.RoomTypes.FirstOrDefault(r => r.RoomTypeId == id);
            return roomType;
            
        }

        public IEnumerable<RoomType> GetRoomType()
        {
            var roomTypes= context.RoomTypes.ToList();
            return roomTypes;
        }

        public void Insert(RoomType obj)
        {
            context.RoomTypes.Add(obj);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(RoomType obj)
        {
            context.Update(obj);
            Save();
        }
    }
}
