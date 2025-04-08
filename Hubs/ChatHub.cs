using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using mvcproj.Models;
using mvcproj.Reporisatory;

namespace mvcproj.Hubs
{
    public class ChatHub:Hub
    {

        private readonly Reservecotexet context;
        private readonly ICurrentUserService currentUserService;

        public ChatHub(Reservecotexet rc , ICurrentUserService uc)
        {
            context = rc;
            currentUserService = uc;
        }

        public async Task SendMessage(string recieverid , string message)

        {
            var NowDate = DateTime.Now;
            var date = NowDate.ToShortDateString();
            var time = NowDate.ToShortTimeString();


            string SenderId = currentUserService.Userid;

            var messageToAdd = new Message()
            {
                Content = message,
                Timestamp = NowDate,
                SenderId = SenderId,
                ReceiverId = recieverid,


            };

            await context.AddAsync(messageToAdd);
            await context.SaveChangesAsync();

            List<string> users = new List<string>()
            {
                recieverid,SenderId
            };

            await Clients.Users(users).SendAsync("ReceiveMessage", message, date, time, SenderId);
        }
    }
}
