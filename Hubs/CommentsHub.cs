using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;

namespace mvcproj.Hubs
{
    public class CommentsHub:Hub
    {
        public async Task NewComment(string guestName, string guestEmail, string CreatedAt, string commentText)
        {
            await Clients.All.SendAsync("ReceiveComment", guestName, guestEmail, CreatedAt, commentText);
        }

    }
}
