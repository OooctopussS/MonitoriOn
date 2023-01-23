namespace MonitoriOn.Models.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Monitor = new Monitor();
        }

        public Monitor? Monitor { get; set; }
        
        public bool ExistsInCart { get; set; }
    }
}
