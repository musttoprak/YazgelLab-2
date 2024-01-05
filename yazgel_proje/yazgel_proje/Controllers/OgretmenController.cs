using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using yazgel_proje.Models;

namespace yazgel_proje.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly MyDbContext _context;

        public OgretmenController(MyDbContext context)
        {
            _context = context;
        }

        // GET: OgretmenController
        public ActionResult Index()
        {
           
            var ogretmenler = _context.Ogretmen.ToList();
            List<Ogretmen>? a = ogretmenler;

            
            return View(ogretmenler);
           
        }

        // GET: OgretmenController/Create
        public ActionResult Create()
        {
            List<DersProgrami> dersler = _context.DersProgrami.ToList();
            ViewBag.DersProgrami = dersler;
            return View();
        }

        // POST: OgretmenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("ogretmenId,ogretmenAdi")] Ogretmen ogretmen) 
        {
            try
            {
                ogretmen.ogretmenId = _context.Ogretmen.ToList().Count;
                _context.Ogretmen.Add(ogretmen);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OgretmenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OgretmenController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: OgretmenController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OgretmenController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OgretmenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
