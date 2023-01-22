using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models
{
    public class FrameUpdate
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите Частоту обновления")]
        [Range(1, int.MaxValue, ErrorMessage = "Укажите корректную частоту обновления")]
        [Display(Name = "Частота обновления")]
        public int Name { get; set; }

        [Display(Name = "Краткое описание")]
        [StringLength(40, ErrorMessage = "Максимальная длина 40 символов")]
        public string? ShortDescription { get; set; }
    }
}
