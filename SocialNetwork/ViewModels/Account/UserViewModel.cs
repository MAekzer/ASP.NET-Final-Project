using SocialNetwork.Models.Users;

namespace SocialNetwork.ViewModels.Account
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<User> Friends { get; set; } = new List<User>();

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}
