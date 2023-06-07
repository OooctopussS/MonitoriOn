using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models
{
    public class BankDetail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Получатель")]
        public string Recipient { get; set; } = null!;

        [Display(Name = "Счет получателя")]
        public string BankAccount { get; set; } = null!;

        [Display(Name = "Банк получателя")]
        public string BankName { get; set; } = null!;

        [Display(Name = "ИНН получателя")]
        public string INN { get; set; } = null!;

        [Display(Name = "Бик банка получателя")]
        public string BankBIC { get; set; } = null!;

        [Display(Name = "Корреспондентский счет")]
        public string CorrespondentAccount { get; set; } = null!;

        [Display(Name = "Адрес подразделения Банка по месту ведения счета карты")]
        public string BankAddress { get; set; } = null!;
    }
}
