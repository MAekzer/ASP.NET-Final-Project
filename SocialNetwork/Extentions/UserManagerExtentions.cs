using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Users;
using System.Collections.Generic;

namespace SocialNetwork.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<List<User>> FindByFullNameAsync(this UserManager<User> manager, string fullName)
        {
            var map = User.GetNameFromFullName(fullName);

            foreach (var pair in map)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }

            IQueryable<User> query;

            if (map.Count == 0)
                throw new BadHttpRequestException("Похоже, в вашем запросе ничего нет", 400);

            map.TryGetValue("firstname", out string? firstname);
            map.TryGetValue("middlename", out string? middlename);
            middlename ??= string.Empty;
            map.TryGetValue("lastname", out string? lastname);
            if (lastname is not null)
                lastname = lastname.ToUpper();
            if (middlename is not null)
                middlename = middlename.ToUpper();
            if (firstname is not null)
                firstname = firstname.ToUpper();

            switch (map.Count)
            {
                case 1:
                    query = manager.Users.Where(u => 
                        (u.FirstName.ToUpper() + " " + u.MiddleName.ToUpper() + " " + u.LastName.ToUpper()).Contains(firstname));

                    break;
                case 2:
                    query = manager.Users.Where(u => 
                        (u.FirstName.ToUpper() + " " + u.MiddleName.ToUpper() + " " + u.LastName.ToUpper()).Contains(firstname + " " + lastname) ||
                        (u.FirstName.ToUpper() + " " + u.MiddleName.ToUpper() + " " + u.LastName.ToUpper()).Contains(firstname + " " + middlename) ||
                        (u.FirstName.ToUpper() + " " + u.MiddleName.ToUpper() + " " + u.LastName.ToUpper()).Contains(middlename + " " + lastname));
                    break;
                case 3:
                    query = manager.Users
                            .Where(u => u.FirstName.ToUpper() == firstname)
                            .Where(u => u.MiddleName.ToUpper() == middlename)
                            .Where(u => u.LastName.ToUpper() == lastname);
                    break;
                default:
                    return new List<User>();
            }

            return await query.ToListAsync();
        }

        public static async Task<List<User>> FindByStatusAsync(this UserManager<User> manager, string status)
        {
            var query = manager.Users.Where(u => u.Status.ToUpper().Contains(status.ToUpper()));

            if (query is null)
                return new List<User>();

            return await query.ToListAsync();
        }

        public static async Task<List<User>> FindByAboutAsync(this UserManager<User> manager, string about)
        {
            var query = manager.Users.Where(u => u.About.ToUpper().Contains(about.ToUpper()));

            if (query is null)
                return new List<User>();

            return await query.ToListAsync();
        }
    }
}
