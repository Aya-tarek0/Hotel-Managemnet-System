namespace mvcproj.View_Models.MessagesViewModels
{
    public class UserMessages
    {

        public int id { set; get; }

        public string text { set; get; }

        public  DateTime date { set; get; }

        public bool isCurrentUserSentMeessage { set; get; }


    }
}
