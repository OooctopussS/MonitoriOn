using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;

namespace MonitoriOn.Controllers
{
    public class SupplyDogovorController : Controller
    {

        private readonly ApplicationDbContext _db;

        public SupplyDogovorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<SupplyDogovor> supplyDogovors = _db.SupplyDogovors.Include(d => d.Account).Include(d => d.Monitors).ThenInclude(b => b.Brand).Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution).Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate);

            return View(supplyDogovors);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(SupplyDogovor supplyDogovor)
        {
            if (ModelState.IsValid)
            {
                supplyDogovor.Monitors ??= new List<Models.Monitor>();

                foreach(var item in supplyDogovor.Monitors)
                {

                    if (supplyDogovor.Account.IsReceived)
                    {
                        item.Sost = 1;
                    }
                    else
                    {
                        item.Sost = 0;
                    }
                }

                _db.SupplyDogovors.Add(supplyDogovor);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(supplyDogovor);
        }

        // GET - MonitorItem
        public IActionResult MonitorItem()
        {
            var monitor = new Models.Monitor();

            return PartialView("_MonitorItems", monitor);
        }

        // GET - AccountInfo
        public IActionResult AccountInfo(SupplyDogovorAccount account)
        {
            return PartialView("_AccountItemModal", account);
        }

        // GET - Monitors
        public IActionResult Monitors(SupplyDogovor obj)
        {
            var supplyDogoovr = _db.SupplyDogovors.Include(d => d.Monitors).ThenInclude(b => b.Brand).Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution).Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate).FirstOrDefault(d => d.Id == obj.Id);

            var monitors = new List<Models.Monitor>();

            if (supplyDogoovr != null)
            {
                monitors = supplyDogoovr.Monitors;
            }

            return PartialView("_MonitorsItemModal", monitors);
        }

        public IActionResult Delete(int id)
        {
            var obj = _db.SupplyDogovors.Include(d => d.Account).Include(d => d.Monitors).FirstOrDefault(i => i.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            if (obj.Account != null)
            {
                var account = _db.SupplyDogovorAccounts.FirstOrDefault(i => i.Id == obj.Account.Id);

                if (account != null)
                    _db.SupplyDogovorAccounts.Remove(account);
            }

            foreach (var item in obj.Monitors)
            {
                if (item.BrandId != 0)
                {
                    var brand = _db.Brands.FirstOrDefault(i => i.Id == item.BrandId);

                    if (brand != null)
                        _db.Brands.Remove(brand);
                }

                if (item.DisplayResolutionId != 0)
                {
                    var resolution = _db.DisplayResolutions.FirstOrDefault(i => i.Id == item.DisplayResolutionId);

                    if (resolution != null)
                        _db.DisplayResolutions.Remove(resolution);
                }

                if (item.FrameUpdaetId != 0)
                {
                    var frame = _db.FrameUpdates.FirstOrDefault(i => i.Id == item.FrameUpdaetId);

                    if (frame != null)
                        _db.FrameUpdates.Remove(frame);
                }

                _db.Monitors.Remove(item);
            }

            _db.SupplyDogovors.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult SetDelivery(int id)
        {
            var supplyDogovor = _db.SupplyDogovors.Include(a => a.Account).Include(m => m.Monitors).FirstOrDefault(d => d.Id == id);

            if (supplyDogovor != null)
            {
                var account = _db.SupplyDogovorAccounts.FirstOrDefault(d => d.Id == supplyDogovor.Account.Id);

                if (account != null)
                {
                    account.IsReceived = true;

                    foreach (var item in supplyDogovor.Monitors)
                    {
                        item.Sost = 1;
                    }

                    _db.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
