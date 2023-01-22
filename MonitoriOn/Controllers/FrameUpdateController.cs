using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonitoriOn.Data;
using MonitoriOn.Models;

namespace MonitoriOn.Controllers
{
    public class FrameUpdateController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FrameUpdateController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<FrameUpdate> FrameUpdateList = _db.FrameUpdates;

            return View(FrameUpdateList);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(FrameUpdate obj)
        {
            if (ModelState.IsValid)
            {
                _db.FrameUpdates.Add(obj);
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

            var obj = _db.FrameUpdates.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(FrameUpdate obj)
        {
            if (ModelState.IsValid)
            {
                _db.FrameUpdates.Update(obj);
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

            var obj = _db.FrameUpdates.Find(id);

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
            var obj = _db.FrameUpdates.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.FrameUpdates.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
