using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.ViewModels
{
    public enum SearchParams
    {
        [Display(Name="Имя пользователя (login)")]
        UserName = 0,
        [Display(Name = "Адрес электронной почты")]
        Email = 1,
        [Display(Name = "Имя пользователя (настоящее имя)")]
        Name = 2,
        [Display(Name = "Статус")]
        Status = 3,
        [Display(Name = "О себе")]
        About = 4,
        [Display(Name = "None")]
        Stop = 5
    }
}
