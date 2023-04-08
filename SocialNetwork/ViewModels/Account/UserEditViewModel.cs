using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.ViewModels.Account
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [Display(Name = "Имя", Prompt = "Введите ваше имя")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [Display(Name = "Имя", Prompt = "Введите вашу фамилию")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Display(Name = "Отчество", Prompt = "Введите отчество при наличии")]
        [DataType(DataType.Text)]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Поле Дата рождения обязательно для заполнения")]
        [Display(Name = "Дата рождения", Prompt = "Введите дату рождения")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [Display(Name = "Email", Prompt = "Введите свой адрес электронной почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Статус", Prompt = "Введите статус")]
        [DataType(DataType.Text)]
        public string? Status { get; set; }
        [Display(Name = "О себе", Prompt = "Введите дополнительную информацию о себе")]
        [DataType(DataType.Text)]
        public string? About { get; set; }
        [Display(Name = "Фото", Prompt = "Введите ссылку на ваше фото")]
        [DataType(DataType.Text)]
        public string? Image { get; set; }
    }
}
