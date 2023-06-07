using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using MonitoriOn.Utility;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipelines;
using System.Security.Claims;
using System.Xml.Linq;

namespace MonitoriOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MonitoriOnUser> _userManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<MonitoriOnUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                Monitors = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate).Where(m => m.Sost == 1),
                Brands = _db.Brands,
                DisplayResolutions = _db.DisplayResolutions,
                FrameUpdates = _db.FrameUpdates
            };

            return View(homeVM);
        }

        public IActionResult Guides()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCartList = new();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)?.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart)!;
            }

            DetailsVM DetailsVM = new()
            {
                Monitor = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate)
                .Where(u => u.Id == id).FirstOrDefault()!,
                ExistsInCart = false
            };

            foreach (var item in shoppingCartList)
            {
                if (item.MonitorId== id)
                {
                    DetailsVM.ExistsInCart = true;
                }
            }

            return View(DetailsVM);
        }


        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCartList = new();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null 
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)?.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart)!;
            }

            int monitorId = id;

            var userId = _userManager.GetUserId(User);

            var monitor = _db.Monitors.FirstOrDefault(m => m.Id == id);

            if (monitor != null && userId != null)
            {
                var newMonitor = new Models.Monitor()
                {
                    Name = monitor.Name,
                    Description = monitor.Description,
                    ShortDescription = monitor.ShortDescription,
                    Price = monitor.Price,
                    Count = 1,
                    BrandId = monitor.BrandId,
                    DisplayResolutionId = monitor.DisplayResolutionId,
                    FrameUpdaetId = monitor.FrameUpdaetId,
                    Image = monitor.Image,
                    BuyerId = userId,
                    Sost = 2,
                    ParentMonitorId = monitor.Id
                };

                _db.Monitors.Add(newMonitor);
                _db.SaveChanges();

                monitorId = newMonitor.Id;
            }

            shoppingCartList.Add(new ShoppingCart { MonitorId= monitorId });
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartList = new();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)?.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart)!;
            }

            var itemToRemove = shoppingCartList.SingleOrDefault(r => r.MonitorId == id);

            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
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