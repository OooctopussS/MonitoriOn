using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using System.Collections.Generic;

namespace MonitoriOn.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReportController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductSold()
        {
            var items = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate).Where(i => i.Sost == 3).ToList();

            return View(items);
        }

        public IActionResult ProductStock()
        {
            var items = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate).Where(i => i.Sost == 1).ToList();

            return View(items);
        }

        public IActionResult SupplyDogovorsConcluded()
        {
            var items = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .ToList();

            return View(items);
        }

        public IActionResult SupplyDogovorsProviders()
        {
            var supplyDogovors = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .ToList();

            var providerList = new List<SupplyDogovorsProvider>();

            foreach (var dogovor in supplyDogovors)
            {
                var provider = providerList.FirstOrDefault(i => i.Provider == dogovor.Provider);

                if (provider != null)
                {
                    provider.Monitors.AddRange(dogovor.Monitors);
                }
                else
                {
                    providerList.Add(new SupplyDogovorsProvider { Provider = dogovor.Provider, Monitors = dogovor.Monitors });
                }
            }

            return View(providerList);
        }

        public IActionResult SupplyDogovorsPaid()
        {
            var items = _db.SupplyDogovorAccounts
                .Where(i => i.IsPaid == true).ToList();

            return View(items);
        }

        public IActionResult SupplyDogovorsDelivery()
        {

            var model = new SupplyDogovorsDeliveryVm();

            var items = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .Where(i => i.Account.IsReceived == true).ToList();

            model.supplyDogovors.AddRange(items);

            return View(model);
        }

        [HttpPost, ActionName("SupplyDogovorsDelivery")]
        [ValidateAntiForgeryToken]
        public IActionResult SupplyDogovorsDeliveryPost(DateTime FromDate, DateTime ToDate)
        {
            var model = new SupplyDogovorsDeliveryVm();
            model.FromDate = FromDate;
            model.ToDate = ToDate;

            var items = new List<SupplyDogovor>();
            if (ToDate == DateTime.MinValue)
            {
                items = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .Where(i => i.Account.IsReceived == true && i.Date >= FromDate).ToList();
            }
            else
            {
                items = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .Where(i => i.Account.IsReceived == true && i.Date >= FromDate && i.Date <= ToDate).ToList();
            }

            model.supplyDogovors.AddRange(items);

            return View(model);
        }

        // GET - AccountInfo
        public IActionResult AccountInfoDelivery(SupplyDogovorAccount account)
        {
            return PartialView("_AccountItemModalDelivery", account);
        }

        // GET - Monitors
        public IActionResult MonitorsDelivery(SupplyDogovor obj)
        {
            var supplyDogoovr = _db.SupplyDogovors.Include(d => d.Monitors).ThenInclude(b => b.Brand).Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution).Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate).FirstOrDefault(d => d.Id == obj.Id);

            var monitors = new List<Models.Monitor>();

            if (supplyDogoovr != null)
            {
                monitors = supplyDogoovr.Monitors;
            }

            return PartialView("_MonitorsItemModal", monitors);
        }

        public IActionResult MonitorsSoldPeriod()
        {
            var model = new MonitorsSoldPeriodVm();
            var items = _db.MonitorsOrders.Include(u => u.Monitors).ToList();

            model.monitorsOrders = items;

            return View(model);
        }

        [HttpPost, ActionName("MonitorsSoldPeriod")]
        [ValidateAntiForgeryToken]
        public IActionResult MonitorsSoldPeriodPost(DateTime FromDate, DateTime ToDate)
        {
            var model = new MonitorsSoldPeriodVm();
            model.FromDate = FromDate;
            model.ToDate = ToDate;

            var items = new List<MonitorsOrder>();
            if (ToDate == DateTime.MinValue)
            {
                items = _db.MonitorsOrders.Include(u => u.Monitors).Where(o => FromDate <= o.Date).ToList();
            }
            else
            {
                items = _db.MonitorsOrders.Include(u => u.Monitors).Where(o => FromDate <= o.Date && o.Date <= ToDate).ToList();
            }

            model.monitorsOrders.AddRange(items);

            return View(model);
        }

        public IActionResult MonitorsPeriodSoldItems(MonitorsOrder monitorOrder)
        {
            var monitors = new List<Models.Monitor>();

            var order = _db.MonitorsOrders.Include(u => u.Monitors).ThenInclude(u => u.Brand).Include(u => u.Monitors).ThenInclude(u => u.DisplayResolution).Include(u => u.Monitors).ThenInclude(u => u.FrameUpdate).FirstOrDefault(o => o.Id == monitorOrder.Id);

            if (order != null)
            {
                monitors = order.Monitors;
            }

            return PartialView("_MonitorsItemModal", monitors);
        }
    }
}
