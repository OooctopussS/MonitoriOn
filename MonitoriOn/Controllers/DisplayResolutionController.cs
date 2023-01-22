using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonitoriOn.Data;
using MonitoriOn.Models;

namespace MonitoriOn.Controllers
{
    public class DisplayResolutionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DisplayResolutionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<DisplayResolution> DisplayResolutionList = _db.DisplayResolutions;

            return View(DisplayResolutionList);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(DisplayResolution obj)
        {
            if (ModelState.IsValid)
            {
                _db.DisplayResolutions.Add(obj);
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

            var obj = _db.DisplayResolutions.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(DisplayResolution obj)
        {
            if (ModelState.IsValid)
            {
                _db.DisplayResolutions.Update(obj);
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

            var obj = _db.DisplayResolutions.Find(id);

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
            var obj = _db.DisplayResolutions.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.DisplayResolutions.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
