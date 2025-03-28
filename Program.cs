using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.Reporisatory;

namespace mvcproj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Reservecotexet>(Contextbuilder =>
            {
                Contextbuilder.UseSqlServer(builder.Configuration.GetConnectionString("db"));
            });
            builder.Services.AddScoped<IRoomTypeReporisatory, RoomTypeReporisatory>();
            builder.Services.AddScoped<IRoomReporisatory, RoomReporisatory>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;


            }).AddEntityFrameworkStores<Reservecotexet>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}