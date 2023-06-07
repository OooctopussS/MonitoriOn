using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models
{
    public class SupplyDogovor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата заключения договора")]
        public DateTime Date { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Условие поставки")]
        public string? DeliveryCondition { get; set; }

        [Display(Name = "Поставщик")]
        public string Provider { get; set; } = null!;

        public SupplyDogovorAccount Account { get; set; } = null!;

        public List<Models.Monitor> Monitors { get; set; } = null!;
    }
}
