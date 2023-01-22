using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models
{
    public class DisplayResolution
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите Разрешение экрана")]
        [StringLength(10, ErrorMessage = "Максимальная длина 10 символов")]
        [Display(Name = "Разрешение экрана")]
        public string Name { get; set; } = null!;

        [Display(Name = "Краткое описание")]
        [StringLength(40, ErrorMessage = "Максимальная длина 40 символов")]
        public string? ShortDescription { get; set; }
    }
}
