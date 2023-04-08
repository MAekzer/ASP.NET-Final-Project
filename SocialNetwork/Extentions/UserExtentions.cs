using Microsoft.EntityFrameworkCore.Update.Internal;
using SocialNetwork.Models.Users;
using SocialNetwork.ViewModels.Account;

namespace SocialNetwork.Extentions
{
    public static class UserExtentions
    {
        public static string GetFullName(this User user)
        {
            if (string.IsNullOrEmpty(user.MiddleName))
                return $"{user.FirstName} {user.LastName}";
            return $"{user.FirstName} {user.MiddleName} {user.LastName}";
        }

        public static void Update(this User user, UserEditViewModel model)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.MiddleName = model.MiddleName;
            user.About = model.About;
            user.Status = model.Status;
            user.Image = model.Image;
            user.BirthDate = model.BirthDate;
        }
    }
}
