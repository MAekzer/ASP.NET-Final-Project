using Microsoft.AspNetCore.Identity;
using SocialNetwork.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
        public string? Status { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }

        /* 
        public User(string firstName, string lastName, string middleName, DateTime birthDate, string status, string about, string image)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDate = birthDate;
            Status = status;
            About = about;
            Image = image;
        }
        */

        public User()
        {
            Image = "https://via.placeholder.com/500";
            Status = "Ура! Я в соцсети!";
            About = "Информация обо мне.";
        }

        public static Dictionary<string, string> GetNameFromFullName(string fullname)
        {
            var map = new Dictionary<string, string>();
            string[] parts = fullname.Split(' ');

            map["firstname"] = parts[0];

            if (parts.Length == 2)
            {
                map["lastname"] = parts[1];
            }
            else if (parts.Length == 3)
            {
                map["middlename"] = parts[1];
                map["lastname"] = parts[2];
            }
            else
            {
                map["firstname"] = fullname;
            }

            return map;
        }
    }
}
