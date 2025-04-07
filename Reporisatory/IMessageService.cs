using Microsoft.AspNetCore.Identity;
using mvcproj.View_Models.MessagesViewModels;

namespace mvcproj.Reporisatory
{
    public interface IMessageService
    {
        Task<IEnumerable<MessagesUserListViewModel>> GetUsers();

        Task<ChatViewModel> GetMessages(string SeletedUserid);
    }
}
