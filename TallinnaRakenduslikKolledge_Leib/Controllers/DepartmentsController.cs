using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledge_Leib.Data;
using TallinnaRakenduslikKolledge_Leib.Models;

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

        // CREATE //
        [HttpGet]
        public IActionResult Create() 
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "FullName");
            ViewData["ViewType"] = "Create";
            //ViewData["CurrentRating"] = new SelectList(_context.Students, "Id", "FirstName", "LastName");
            return View("CreateEdit");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "FullName");
            ViewData["ViewType"] = "Edit";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return View("CreateEdit", department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("DepartmentId,Name,Budget,StartDate,RowVersion,InstructorId,CurrentRating")] Department department, string ViewType)
        {
            if (ModelState.IsValid)
            {
                if (ViewType == "Edit")
                {
                    _context.Update(department);

                }
                else
                {
                    _context.Add(department);
                }
                await _context.SaveChangesAsync();
            }
            if (ViewType == "Edit")
            {
                return View(department);
			}
            else
            {
                ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "FullName", department.InstructorId);
                return RedirectToAction("Index");
            }
            //ViewData["CurrentRating"] = new SelectList(_context.Instructors, "Id", department.CurrentRating.ToString(), department.CurrentRating);
        }

        // DELETE //
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "FullName");
            ViewData["ViewType"] = "Delete";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return View("DeleteDetails", department);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "FullName");
            ViewData["ViewType"] = "Details";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return View("DeleteDetails", department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Department department)
        {
            if (await _context.Departments.AnyAsync(m => m.DepartmentId == department.DepartmentId)) 
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
