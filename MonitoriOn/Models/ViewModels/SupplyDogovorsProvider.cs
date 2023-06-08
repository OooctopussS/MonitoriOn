using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonitoriOn.Models.ViewModels
{
    public class SupplyDogovorsProvider
    {
        [Display(Name = "Поставщик")]
        public string? Provider { get; set; }

        public List<Models.Monitor> Monitors { get; set; } = new List<Models.Monitor>();
    }
}
