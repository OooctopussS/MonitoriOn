using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoriOn.Models
{
    public class Monitor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название Монитора")]
        [Display(Name = "Название Монитора")]
        public string Name { get; set; } = null!;

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Краткое описание")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Цена")]
        [Column(TypeName = "money")]
        [Range(1, int.MaxValue, ErrorMessage = "Введите корректную цену")]
        public decimal Price { get; set; }

        [Display(Name = "Количество товара")]
        [Range(0, int.MaxValue, ErrorMessage = "Введите корректное значение")]
        public int Count { get; set; }


        [Display(Name = "Бренд*")]
        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public virtual Brand? Brand { get; set; }

        [Display(Name = "Разрешение экрана*")]
        public int DisplayResolutionId { get; set; }
        [ForeignKey(nameof(DisplayResolutionId))]
        public virtual DisplayResolution? DisplayResolution { get; set; }

        [Display(Name = "Частота обновления*")]
        public int FrameUpdaetId { get; set; }
        [ForeignKey(nameof(FrameUpdaetId))]
        public virtual FrameUpdate? FrameUpdate { get; set; }

        [Display(Name = "Изображение")]
        public string? Image { get; set; }
    }
}
