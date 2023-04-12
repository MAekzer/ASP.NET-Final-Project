using SocialNetwork.Models.Users;

namespace SocialNetwork.ViewModels.Account
{
    public class FriendsViewModel
    {
        public List<User> Friends { get; set; } = new List<User>();
    }
}
