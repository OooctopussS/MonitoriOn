using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MonitoriOn.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Это поле обязательное")]
        [EmailAddress(ErrorMessage = "Введите корректную почту")]
        [Display(Name = "Почта*")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательное")]
        [StringLength(30, ErrorMessage = "Пароль должен быть длиной от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль не совпадает с указанным")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string Code { get; set; } = null!;
    }
}
