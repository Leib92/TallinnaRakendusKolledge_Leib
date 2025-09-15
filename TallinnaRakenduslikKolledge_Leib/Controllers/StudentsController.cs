using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledge_Leib.Data;
using TallinnaRakenduslikKolledge_Leib.Models;

namespace TallinnaRakenduslikKolledge_Leib.Controllers
{
    public class StudentsController : Controller
    {
        // CONTEXT
        private readonly SchoolContext _context;
        public StudentsController(SchoolContext context) 
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind
            ("ID,LastName,FirstName,EnrollmentDate,Email")] Student student)
        {
            if (ModelState.IsValid) 
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Alternative: "return RedirectToAction(nameof(Index))"

            }
            return View(student);
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // DETAIL
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowDetail(int id)
        {
            return View(await _context.Students.ToListAsync());
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
