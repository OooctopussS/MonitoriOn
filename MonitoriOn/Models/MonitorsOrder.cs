using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoriOn.Models
{
    public class MonitorsOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Сумма")]
        public decimal Amount { get; set; }

        [Display(Name = "Адрес доставки")]
        public string? UserAddress { get; set; }

        [Display(Name = "Банковская карта")]
        public string Account { get; set; } = null!;

        [Display(Name = "CVV")]
        public string? AccountCVV { get; set; } = null!;

        public bool IsPickUp { get; set; }

        public bool IsDelivery { get; set; }

        public List<Models.Monitor>? Monitors { get; set; }
    }
}
