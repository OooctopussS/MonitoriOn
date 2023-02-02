using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MonitoriOn.Models.ViewModels
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Это поле обязательное")]
        [StringLength(30, ErrorMessage = "Пароль должен быть длиной от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательное")]
        [StringLength(30, ErrorMessage = "Пароль должен быть длиной от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Это поле обязательное")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Пароль не совпадает с указанным")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
