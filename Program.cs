using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvcproj.Hubs;
using mvcproj.Models;
using mvcproj.Reporisatory;
using System.Security.Policy;

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
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<ICommentReporisatory, CommentReporisatory>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IMessageService, MessageService>();





            builder.Services.AddSignalR();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;


            }).AddEntityFrameworkStores<Reservecotexet>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(Url => true)
                    .AllowCredentials();
                });
            });

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

            app.UseCors();
            
            app.MapHub<CommentsHub>("/CommentHub");
            app.MapHub<ChatHub>("/ChatHub");


            app.UseRouting();


            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}