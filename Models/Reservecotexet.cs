using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace mvcproj.Models
{
    public class Reservecotexet : IdentityDbContext<ApplicationUser>
    {
        public Reservecotexet(DbContextOptions<Reservecotexet> options) : base(options)
        { }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Resturatnt> Resturatnts { set; get; }



    }
}
