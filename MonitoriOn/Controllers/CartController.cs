using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using MonitoriOn.Utility;

namespace MonitoriOn.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
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

            List<int> monitorInCart = shoppingCartList.Select(i => i.MonitorId).ToList();

            IEnumerable<Models.Monitor> monitorList = _db.Monitors.Where(u => monitorInCart.Contains(u.Id)).Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate);


            List<CartVM> cartMonitors = new();

            foreach (var monitor in monitorList)
            {
                cartMonitors.Add(new CartVM { MonitorItem = monitor, NeedToBuy = true });
            }



            return View(cartMonitors);
        }
    }
}
