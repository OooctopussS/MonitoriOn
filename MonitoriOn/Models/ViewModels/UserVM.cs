using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models.ViewModels
{
    public class UserVM
    {
        [MaxLength(20, ErrorMessage = "Максимальное кол-во символов {1}")]
        [MinLength(2, ErrorMessage = "Минимальное кол-во символов {1}")]
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [Display(Name = "Адрес")]
        public string? Address { get; set; }

        [DisplayName("Почта")]
        [EmailAddress(ErrorMessage = "Введите корректную почту")]
        public string? Email { get; set; }

        [DisplayName("Телефон")]
        [Phone(ErrorMessage = "Введите корректный номер")]

        public string? PhoneNumber { get; set; }
    }
}
