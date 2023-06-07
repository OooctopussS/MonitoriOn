using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models;
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
                .Where(i => i.Account.IsReceived == false).ToList();

            return View(items);
        }

        public IActionResult SupplyDogovorsPaid()
        {
            var items = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .Where(i => i.Account.IsPaid == true).ToList();

            return View(items);
        }

        public IActionResult SupplyDogovorsDelivery()
        {
            var items = _db.SupplyDogovors.Include(d => d.Account)
                .Include(d => d.Monitors).ThenInclude(b => b.Brand)
                .Include(d => d.Monitors).ThenInclude(b => b.DisplayResolution)
                .Include(d => d.Monitors).ThenInclude(b => b.FrameUpdate)
                .Where(i => i.Account.IsReceived == true).ToList();

            return View(items);
        }
    }
}
