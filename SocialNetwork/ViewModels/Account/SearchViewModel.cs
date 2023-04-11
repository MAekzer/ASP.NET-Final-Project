using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SocialNetwork.Models.Users;

namespace SocialNetwork.ViewModels.Account
{
    public class SearchViewModel
    {
        public SearchParams SearchParam { get; set; }
        public string Value { get; set; }

        public List<User> Result { get; set; } = new List<User>();
        public List<User> Friends { get; set; } = new List<User>();
    }
}
