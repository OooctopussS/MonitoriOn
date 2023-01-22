using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using System.Diagnostics;

namespace MonitoriOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                Monitors = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate),
                Brands = _db.Brands,
                DisplayResolutions = _db.DisplayResolutions,
                FrameUpdates = _db.FrameUpdates
            };

            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}