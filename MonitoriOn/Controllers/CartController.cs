using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using MonitoriOn.Utility;
using System.Linq;
using System.Threading;

namespace MonitoriOn.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<MonitoriOnUser> _userManager;


        public CartController(ApplicationDbContext db, UserManager<MonitoriOnUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)?.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart)!;
            }

            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                var oldCartMonitors = _db.Monitors.Where(m => m.BuyerId == userId && m.Sost == 2).ToList();

                foreach (var item in oldCartMonitors)
                {
                    shoppingCartList.Add(new ShoppingCart { MonitorId = item.Id });
                }
            }

            List<int> monitorInCart = shoppingCartList.Select(i => i.MonitorId).ToList();

            IEnumerable<Models.Monitor> monitorList = _db.Monitors.Where(u => monitorInCart.Contains(u.Id)).Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate);


            List<CartVM> cartMonitors = new();

            foreach (var monitor in monitorList)
            {
                cartMonitors.Add(new CartVM { MonitorItem = monitor, NeedToBuy = true });
            }



            return View(cartMonitors);
        }

        // GET - Delivery
        public IActionResult Delivery()
        {
            return PartialView("_Delivery");
        }

        public IActionResult Delete(int id)
        {
            List<ShoppingCart> shoppingCartList = new();

            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                var monitor = _db.Monitors.FirstOrDefault(m => m.Id == id);

                if (monitor != null)
                {
                    _db.Monitors.Remove(monitor);
                    _db.SaveChanges();
                }
            }

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)?.Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart)!;
            }

            var sessionItem = shoppingCartList.Find(m => m.MonitorId == id);

            if (sessionItem != null)
            {
                shoppingCartList.Remove(sessionItem);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult BuyMonitors(MonitorsOrder monitorsOrder)
        {
            if (ModelState.IsValid)
            {
                List<ShoppingCart> shoppingCartList = new();

                if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)?.Count() > 0)
                {
                    shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart)!;
                }

                var userId = _userManager.GetUserId(User);

                if (userId != null)
                {
                    var oldCartMonitors = _db.Monitors.Where(m => m.BuyerId == userId && m.Sost == 2).ToList();

                    foreach (var item in oldCartMonitors)
                    {
                        shoppingCartList.Add(new ShoppingCart { MonitorId = item.Id });
                    }
                }

                List<int> idsList = new List<int>();

                foreach (var obj in shoppingCartList)
                {
                    idsList.Add(obj.MonitorId);
                }

                var monitors = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate).Where(m => idsList.Contains(m.Id));

                monitorsOrder.Monitors = new List<Models.Monitor>();

                foreach (var item in monitors)
                {
                    var newMonitor = new Models.Monitor
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Sost = 3,
                        Count = 1,
                        Brand = item.Brand,
                        BrandId = item.BrandId,
                        DisplayResolution = item.DisplayResolution,
                        DisplayResolutionId = item.DisplayResolutionId,
                        FrameUpdate = item.FrameUpdate,
                        FrameUpdaetId = item.FrameUpdaetId
                    };

                    monitorsOrder.Amount += item.Price;

                    if (item.Sost == 1 && item.Count > 0)
                    {
                        item.Count--;
                    }

                    if (item.Sost == 2)
                    {
                        _db.Monitors.Remove(item);
                    }

                    monitorsOrder.Monitors.Add(newMonitor);
                }

                monitorsOrder.Date = DateTime.Now;

                _db.MonitorsOrders.Add(monitorsOrder);
                _db.SaveChanges();


                HttpContext.Session.Set(WC.SessionCart, new List<ShoppingCart>());

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
