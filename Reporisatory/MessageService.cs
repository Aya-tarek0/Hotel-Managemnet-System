using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using mvcproj.Models;
using mvcproj.View_Models.MessagesViewModels;

namespace mvcproj.Reporisatory
{
    public class MessageService : IMessageService

    {

        Reservecotexet context;
        ICurrentUserService currentservice;

        public MessageService(Reservecotexet rc, ICurrentUserService currentUserService)
        {
            context = rc;
            currentservice = currentUserService;

        }

        public async Task<ChatViewModel> GetMessages(string SeletedUserid)
        {
            var currentid = currentservice.Userid;
            var selectesuser = await context.Users.FirstOrDefaultAsync(i => i.Id == SeletedUserid);
            var selectedusername = "";
            if (selectesuser != null)  
            {
                selectedusername = selectesuser.UserName;
            }

            var messsages = await context.Messages
                .Where(i => (i.Sender.Id == currentid || i.Sender.Id == SeletedUserid) &&
                            (i.ReceiverId == currentid || i.ReceiverId == SeletedUserid))
                .OrderBy(i => i.Timestamp)
                .Select(i => new UserMessages()
                {
                    id = i.Id,
                    text = i.Content,
                    date = i.Timestamp,
                    isCurrentUserSentMeessage = i.Sender.Id == currentid,
                })
                .ToListAsync();

            var chatView = new ChatViewModel()
            {
                CurrentUserId = currentid,
                RecieverId = SeletedUserid,
                RecieverUserName = selectedusername,
                Messages = messsages
            };

            return chatView;
        }

        public async Task<IEnumerable<MessagesUserListViewModel>> GetUsers()
        {
            var currentuserid = currentservice.Userid;

            var users = await context.Users
                .Where(e => e.Id != currentuserid)
                .Select(e => new MessagesUserListViewModel
                {
                    id = e.Id,
                    UserName = e.UserName,
                    LastMessage = context.Messages
                        .Where(m =>
                            (m.Sender.Id == currentuserid && m.ReceiverId == e.Id) ||
                            (m.Sender.Id == e.Id && m.ReceiverId == currentuserid))
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Content)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return users;
        }

    }
}
