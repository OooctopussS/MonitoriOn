using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoriOn.Models.ViewModels
{
    public class MonitorsSoldPeriodVm
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<MonitorsOrder> monitorsOrders { get; set; } = new List<MonitorsOrder>();
    }
}
