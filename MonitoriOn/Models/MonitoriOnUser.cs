using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MonitoriOn.Models
{
    public class MonitoriOnUser : IdentityUser
    {
        [Required(ErrorMessage = "Это поле обязательное")]
        [MaxLength(30, ErrorMessage = "Максимальное кол-во символов {1}")]
        [Display(Name = "Имя")]
        [PersonalData]
        public string FirstName { get; set; } = null!;
    }
}
