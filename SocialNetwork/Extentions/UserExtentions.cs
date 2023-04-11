using Microsoft.EntityFrameworkCore.Update.Internal;
using SocialNetwork.Models.Users;
using SocialNetwork.ViewModels.Account;
using System.Runtime.CompilerServices;

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

        public static int GetIntAge(this User user)
        {
            return DateTime.Now.Year - user.BirthDate.Year;
        }

        public static string GetStrAge(this User user)
        {
            var age = user.GetIntAge();
            var modulo = age % 10;

            Dictionary<int, string> map = new();

            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i >= 5) map[i] = "лет";
                else if (i == 1) map[i] = "год";
                else map[i] = "года";
            }

            return $"{age} {map[modulo]}";
        }

        public static int GetBirthYearFromAge(this User user, int age)
        {
            return DateTime.Now.Year - age;
        }
    }
}
