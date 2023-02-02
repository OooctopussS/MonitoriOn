using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MonitoriOn.Models.ViewModels
{
    public class LoginUserVM
    {
        [Required(ErrorMessage = "Это поле обязательное")]
        [Display(Name = "Почта")]
        [EmailAddress(ErrorMessage = "Введите корректную почту")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательное")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
