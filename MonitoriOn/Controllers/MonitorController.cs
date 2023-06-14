using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Data;
using MonitoriOn.Models.ViewModels;
using System.Runtime.CompilerServices;

namespace MonitoriOn.Controllers
{
    public class MonitorController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MonitorController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.Monitor> objList = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate).Where(m => m.Sost == 1);

            return View(objList);
        }


        //GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            MonitorVM monitorVM = new()
            {
                Monitor = new Models.Monitor(),
                BrandSelectList = _db.Brands.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                DisplayResolutionSelectList = _db.DisplayResolutions.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                FrameUpdateSelectList = _db.FrameUpdates.Select(i => new SelectListItem
                {
                    Text = i.Name.ToString(),
                    Value = i.Id.ToString()
                })
            };
            
            if (id == null)
            {
                // Создание новго монитора

                return View(monitorVM);
            }
            else
            {
                // Редактирование
                var findObj = _db.Monitors.Find(id);

                if (findObj == null)
                {
                    return NotFound();
                }

                monitorVM.Monitor = findObj;

                return View(monitorVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MonitorVM monitorVM)
        {
            if (ModelState.IsValid) 
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (monitorVM.Monitor.Id == 0)
                {
                    // Ветка с созданием

                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    monitorVM.Monitor.Image = fileName + extension;
                    monitorVM.Monitor.Sost = 1;

                    _db.Monitors.Add(monitorVM.Monitor);
                }
                else
                {
                    // Ветка с обновлением

                    var objFromDb = _db.Monitors.AsNoTracking().FirstOrDefault(u => u.Id == monitorVM.Monitor.Id);

                    if (objFromDb != null)
                    {
                        if (files.Count > 0 && objFromDb.Image != null)
                        {
                            string upload = webRootPath + WC.ImagePath;
                            string fileName = Guid.NewGuid().ToString();
                            string extension = Path.GetExtension(files[0].FileName);

                            var oldFile = Path.Combine(upload, objFromDb.Image);

                            if (System.IO.File.Exists(oldFile))
                            {
                                System.IO.File.Delete(oldFile);
                            }

                            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);
                            }

                            monitorVM.Monitor.Image = fileName + extension;
                        }
                        else
                        {
                            if (files.Count > 0)
                            {
                                string upload = webRootPath + WC.ImagePath;
                                string fileName = Guid.NewGuid().ToString();
                                string extension = Path.GetExtension(files[0].FileName);

                                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                                {
                                    files[0].CopyTo(fileStream);
                                }

                                monitorVM.Monitor.Image = fileName + extension;
                            }
                            else
                            {
                                monitorVM.Monitor.Image = objFromDb.Image;
                            }
                        }

                        monitorVM.Monitor.Sost = 1;

                        _db.Monitors.Update(monitorVM.Monitor);
                    }
                }

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            monitorVM.BrandSelectList = _db.Brands.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            monitorVM.DisplayResolutionSelectList = _db.DisplayResolutions.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            monitorVM.FrameUpdateSelectList = _db.FrameUpdates.Select(i => new SelectListItem
            {
                Text = i.Name.ToString(),
                Value = i.Id.ToString()
            });

            return View(monitorVM);
        }

        //GET - DELETE

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Models.Monitor? monitor = _db.Monitors.Include(u => u.Brand).Include(u => u.DisplayResolution).Include(u => u.FrameUpdate).FirstOrDefault(i => i.Id == id);


            if (monitor == null)
            {
                return NotFound();
            }

            return View(monitor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Monitors.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            if (obj.Image != null)
            {
                string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

                var oldFile = Path.Combine(upload, obj.Image);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
            }

            _db.Monitors.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
