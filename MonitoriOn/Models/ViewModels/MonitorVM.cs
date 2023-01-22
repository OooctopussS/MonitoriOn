using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MonitoriOn.Models.ViewModels
{
    public class MonitorVM
    {
        public Monitor Monitor { get; set; } = null!;

        public IEnumerable<SelectListItem>? BrandSelectList { get; set; }

        public IEnumerable<SelectListItem>? DisplayResolutionSelectList { get; set; }

        public IEnumerable<SelectListItem>? FrameUpdateSelectList { get; set; }

    }
}
