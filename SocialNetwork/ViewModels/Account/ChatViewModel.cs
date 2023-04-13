using SocialNetwork.Models.Users;

namespace SocialNetwork.ViewModels.Account
{
    public class ChatViewModel
    {
        public User Current { get; set; }
        public User Other { get; set; }
        public List<Message> Messages { get; set; }
        public string NewMessage { get; set; }
    }
}
