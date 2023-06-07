using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonitoriOn.Data;
using MonitoriOn.Models;

namespace MonitoriOn.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BrandController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Brand> brandsList = _db.Brands.Include(u => u.BankDetail);

            return View(brandsList);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Brand obj)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Add(obj);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        // GET - EDIT
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var obj = _db.Brands.Include(u => u.BankDetail).FirstOrDefault(i => i.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Brand obj)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Update(obj);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        // GET - DELETE
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var obj = _db.Brands.Include(u => u.BankDetail).FirstOrDefault(i => i.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var obj = _db.Brands.Include(u => u.BankDetail).FirstOrDefault(i => i.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            if (obj.BankDetail != null)
            {
                var bankDetail = _db.BankDetail.FirstOrDefault(i => i.Id == obj.BankDetail.Id);

                if (bankDetail != null)
                    _db.BankDetail.Remove(bankDetail);
            }


            _db.Brands.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        // GET - BankDetail
        public IActionResult BankDetail(BankDetail bankDetail)
        {
            return PartialView("_BankDetailModal", bankDetail);
        }

    }
}
