using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле Email є обов'язковим")]
        [EmailAddress(ErrorMessage = "Недійсний формат Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль є обов'язковим")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }

}