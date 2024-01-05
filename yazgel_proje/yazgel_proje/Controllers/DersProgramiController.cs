using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Data.Entity;
using System.Diagnostics;
using yazgel_proje.Models;

namespace yazgel_proje.Controllers
{
    public class DersProgramiController : Controller
    {
        private readonly MyDbContext _context;

        public DersProgramiController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var a = _context.DersProgrami.ToList();
            return View(a);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DersProgrami == null)
            {
                return NotFound();
            }

            var dersProgrami = _context.DersProgrami.Where(m => m.dersProgramiId == id).FirstOrDefault();
            if (dersProgrami == null)
            {
                return NotFound();
            }

            return View(dersProgrami);
        }
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("dersProgramiId,ogretmenAdi,dersAdi,sinif")] DersProgrami dersProgrami)
        {
             dersProgrami.dersProgramiId = _context.DersProgrami.ToList().Count;
             _context.DersProgrami.Add(dersProgrami);
            await _context.SaveChangesAsync();

            DersGraph dersGraph = new DersGraph();
            int count = _context.Kisit.ToList().Count;
            Kisit kisit;
            foreach (var item in _context.Kisit)
            {
                dersGraph.graph.Add(new Dugum(item));
            }
            int kenarVarMı = 1;
            Random rnd1 = new Random();
            int rastgeleGunIndex1 = rnd1.Next(1, 6);
            int rastgelesaatIndex1 = rnd1.Next(9, 16);
            
            int rastgelesınıf1 = rnd1.Next(1, 3);
            string secilenGun1 = GunIndexToGunAdi(rastgeleGunIndex1);

            kisit = new Kisit(count, dersProgrami.dersAdi, secilenGun1, rastgelesaatIndex1.ToString(), dersProgrami.sinif, dersProgrami.ogretmenAdi.ToString(), -1, -1,dersProgrami.dersProgramiId);
            kenarVarMı = dersGraph.HasConflict(new Dugum(kisit));

            while (kenarVarMı == 1 || kenarVarMı == 2)
            {
                Random rnd = new Random();
                int rastgeleGunIndex = rnd.Next(1, 6);
                int rastgelesaatIndex = rnd.Next(9, 16);
                
                int rastgelesınıf = rnd.Next(1, 3);
                string secilenGun = GunIndexToGunAdi(rastgeleGunIndex);

                kisit = new Kisit(count, dersProgrami.dersAdi, secilenGun, rastgelesaatIndex.ToString(), dersProgrami.sinif, dersProgrami.ogretmenAdi.ToString(), -1, -1, dersProgrami.dersProgramiId);
                kenarVarMı = dersGraph.HasConflict(new Dugum(kisit));
                Console.WriteLine(kenarVarMı + "Tip hatası ile tekrardan oluşturulacak");
            }

            _context.Kisit.Add(kisit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DersProgrami == null)
            {
                return NotFound();
            }

            var dersProgrami = await _context.DersProgrami.FindAsync(id);
            if (dersProgrami == null)
            {
                return NotFound();
            }
            return View(dersProgrami);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("dersProgramiId,ogretmenAdi,dersAdi,sinif")] DersProgrami dersProgrami)
        {
            if (id != dersProgrami.dersProgramiId)
            {
                return NotFound();
            }

            try
            {
                _context.DersProgrami.Update(dersProgrami);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DersProgramiExists(dersProgrami.dersProgramiId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var dersProgrami = await _context.DersProgrami.FindAsync(id);

                if (dersProgrami != null)
                {
                    _context.DersProgrami.Remove(dersProgrami);
                    await _context.SaveChangesAsync();
                }


                var kisit = await _context.Kisit.FindAsync(id);

                if (kisit != null)
                {
                    _context.Kisit.Remove(kisit);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DersProgramiExists(int id)
        {
          return (_context.DersProgrami?.Any(e => e.dersProgramiId == id)).GetValueOrDefault();
        }

        static string GunIndexToGunAdi(int gunIndex)
        {
            // Verilen gün indeksini gün adına çeviren bir fonksiyon
            switch (gunIndex)
            {
                case 1:
                    return "Pazartesi";
                case 2:
                    return "Salı";
                case 3:
                    return "Çarşamba";
                case 4:
                    return "Perşembe";
                case 5:
                    return "Cuma";
                default:
                    return "Geçersiz gün";
            }
        }
    }
}
