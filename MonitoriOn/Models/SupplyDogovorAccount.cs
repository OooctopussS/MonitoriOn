using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models
{
    public class SupplyDogovorAccount
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Номер счета")]
        public string AccountNumber { get; set; } = null!;

        [Display(Name = "Дата продажи")]
        public DateTime Date { get; set; }

        [Display(Name = "Сумма")]
        public decimal Amount { get; set; }

        [Display(Name = "НДС")]
        public decimal VAT { get; set; }

        [Display(Name = "Договор оплачен?")]
        public bool IsPaid { get; set; }

        [Display(Name = "Товар договора получен?")]
        public bool IsReceived { get; set; }
    }
}
