using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Reservecotexet context;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor,Reservecotexet context)
        {
            _httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        public string Userid
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
                if(string.IsNullOrEmpty(id))
                {
                    id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                return id ?? string.Empty;
            }
        }


        public async Task<ApplicationUser?> GetUser()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == Userid);
            return user;
        }
    }
}