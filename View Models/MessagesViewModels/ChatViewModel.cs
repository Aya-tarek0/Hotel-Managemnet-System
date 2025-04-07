namespace mvcproj.View_Models.MessagesViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Messages = new List<UserMessages>();
        }

        public string CurrentUserId { set; get; }
        public string RecieverId { set; get; }
        public string RecieverUserName { set; get; }

        public IEnumerable<UserMessages> Messages { set; get; }
    }
}
