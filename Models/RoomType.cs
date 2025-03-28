namespace mvcproj.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public int TypeID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ?PricePerNight { get; set; }
        public int? Capacity { get; set; }

        public List<Room>? Rooms { get; set; }
    }
}
