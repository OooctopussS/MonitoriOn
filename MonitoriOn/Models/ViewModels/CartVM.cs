namespace MonitoriOn.Models.ViewModels
{
    public class CartVM
    {
        public Monitor MonitorItem { get; set; } = null!;

        public bool NeedToBuy { get; set; }
    }
}
