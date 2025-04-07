using mvcproj.Models;

namespace mvcproj.Reporisatory
{
    public interface ICurrentUserService
    {
        string Userid { get; }
        Task<ApplicationUser> GetUser();
    }
}
