namespace MonitoriOn.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Monitor>? Monitors { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public IEnumerable<DisplayResolution>? DisplayResolutions { get; set; }
        public IEnumerable<FrameUpdate>? FrameUpdates { get; set; }
    }
}
