using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MonitoriOn.Models.ViewModels
{
    public class RegisterUserVM
    {
        [Required(ErrorMessage = "Это поле обязательное")]
        [MaxLength(20, ErrorMessage = "Максимальное кол-во символов {1}")]
        [MinLength(2, ErrorMessage = "Минимальное кол-во символов {1}")]
        [Display(Name = "Имя*")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательное")]
        [EmailAddress(ErrorMessage = "Введите корректную почту")]
        [Display(Name = "Почта*")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Укажите корректный номер")]
        [Display(Name = "Номер телефона")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Это поле обязательное")]
        [StringLength(30, ErrorMessage = "Пароль должен быть длиной от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль не совпадает с указанным")]
        public string? ConfirmPassword { get; set; }

        
    }
}
