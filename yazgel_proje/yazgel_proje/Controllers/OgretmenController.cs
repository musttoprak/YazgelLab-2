using Microsoft.AspNetCore.Mvc;
using System.Linq;
using yazgel_proje.Models;

namespace yazgel_proje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OgretmenController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OgretmenController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOgretmenler()
        {
            var ogretmenler = _context.Ogretmen.ToList();
            Console.WriteLine(ogretmenler.Capacity);
            return Ok(ogretmenler);
        }
    }
}
