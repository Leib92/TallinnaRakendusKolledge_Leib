using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledge_Leib.Data;

namespace TallinnaRakenduslikKolledge_Leib.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;
        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var SchoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await SchoolContext.ToListAsync());
        }
    }
}
