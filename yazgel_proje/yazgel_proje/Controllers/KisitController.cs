using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yazgel_proje.Models;

namespace yazgel_proje.Controllers
{
    public class KisitController : Controller
    {
        // GET: KisitController
        private readonly MyDbContext _context;

        public KisitController(MyDbContext context)
        {
            _context = context;
        }


        public async Task<ActionResult> Index()
        {
            DersGraph dersGraph = new DersGraph();
            foreach (var item in _context.Kisit)
            {
                dersGraph.graph.Add(new Dugum(item));
            }
            dersGraph = siralamaYap(dersGraph);

            List<Kisit> dersler = [];
            foreach (var item in dersGraph.graph)
            {
                dersler.Add(item.kisit);
            }

            _context.Kisit.RemoveRange(_context.Kisit);
            await _context.SaveChangesAsync();
            _context.Kisit.AddRange(dersler);
            await _context.SaveChangesAsync();

            return View(_context.Kisit.ToList());
        }

        


        // GET: KisitController/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View(_context.Kisit.Where(x => x.kisitId == id).FirstOrDefault());
        }

        // POST: KisitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Edit(int id, [Bind("kisitId,dersAdi,gun,saat,sinif,ogretmenAdi,ustKisitId,altKisitId,dersProgramiId")] Kisit kisit)
        {
            try
            {
                DersGraph dersGraph = new DersGraph();
                foreach (var item in _context.Kisit)
                {
                    dersGraph.graph.Add(new Dugum(item));
                }
                dersGraph = siralamaYap(dersGraph);

                
                int kenarVarMı = dersGraph.HasConflict(new Dugum(kisit));
                if (kenarVarMı==1)
                {
                    ModelState.AddModelError("", "Seçtiğiniz zaman diliminde çakışma var, lütfen başka bir zaman dilimi seçiniz.");
                    return View(kisit);
                }
                else if (kenarVarMı == 2)
                {
                    ModelState.AddModelError("", "Seçtiğiniz zaman diliminde, farklı sınıflarda aynı Öğretmen ders veremez.");
                    return View(kisit);
                }
                else
                {
                    dersGraph.graph.Where(x => x.kisit.kisitId == kisit.kisitId).FirstOrDefault().kisit = kisit;   
                    dersGraph = siralamaYap(dersGraph);
                    List<Kisit> dersler = [];
                    foreach (var item in dersGraph.graph)
                    {
                        dersler.Add(item.kisit);
                    }
                    _context.Kisit.RemoveRange(_context.Kisit);
                    await _context.SaveChangesAsync();
                    _context.Kisit.AddRange(dersler);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
        public DersGraph siralamaYap(DersGraph dersGraph)
        {
            dersGraph.graph.Sort((x, y) =>
            {
                var gunler = new List<string> { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };

                // Günleri karşılaştır
                var gunKarsilastirma = gunler.IndexOf(x.kisit.gun).CompareTo(gunler.IndexOf(y.kisit.gun));

                if (gunKarsilastirma != 0)
                {
                    return gunKarsilastirma; // Günler farklı ise doğrudan gün sıralamasını kullan
                }
                else
                {
                    // Aynı gün ise saatlere göre sırala
                    return x.kisit.saat.CompareTo(y.kisit.saat);
                }
            });

            for (int i = 0; i < dersGraph.graph.Count; i++)
            {
                Kisit altKisit = null;
                Kisit ustKisit = null;

                if (i - 1 >= 0) // Alt indeks kontrolü
                {
                    altKisit = dersGraph.graph[i - 1].kisit;
                    dersGraph.graph[i].kenarlar.altKisit = altKisit;
                    dersGraph.graph[i].kisit.altKisitId = altKisit.kisitId;
                }

                if (i + 1 < dersGraph.graph.Count) // Üst indeks kontrolü
                {
                    ustKisit = dersGraph.graph[i + 1].kisit;
                    dersGraph.graph[i].kenarlar.ustKisit = ustKisit;
                    dersGraph.graph[i].kisit.ustKisitId = ustKisit.kisitId;
                }
            }

            foreach (var kisit in dersGraph.graph)
            {
                Console.WriteLine($"Gün: {kisit.kisit.gun}, Saat: {kisit.kisit.saat} , Id: {kisit.kisit.kisitId}");
                Console.WriteLine($"ust: {kisit.kisit.ustKisitId}, alt: {kisit.kisit.altKisitId}");
            }

            return dersGraph;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var kisit = await _context.Kisit.FindAsync(id);

                if (kisit != null)
                {
                    _context.Kisit.Remove(kisit);
                    await _context.SaveChangesAsync();
                }


                var dersProgrami = await _context.DersProgrami.FindAsync(kisit.dersProgramiId);

                if (dersProgrami != null)
                {
                    _context.DersProgrami.Remove(dersProgrami);
                    await _context.SaveChangesAsync();
                }
            }
          
            return RedirectToAction(nameof(Index));
        }
    }
}
