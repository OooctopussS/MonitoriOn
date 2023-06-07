using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название Бренда")]
        [StringLength(10, ErrorMessage = "Максимальная длина 10 символов")]
        [Display(Name = "Название Бренда")]
        public string Name { get; set; } = null!;

        [Display(Name = "Краткое описание")]
        [StringLength(40, ErrorMessage = "Максимальная длина 40 символов")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Адрес")]
        public string? Address { get; set; }

        [Display(Name = "Директор")]
        public string? Director { get; set; }

        [Display(Name = "Главный бухгалтер")]
        public string? MainAccountant { get; set; }

        [Display(Name = "Банковсие реквизиты")]
        public BankDetail? BankDetail { get; set; }
    }
}
