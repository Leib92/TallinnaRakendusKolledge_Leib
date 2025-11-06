using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledge_Leib.Data;
using TallinnaRakenduslikKolledge_Leib.Models;

namespace TallinnaRakenduslikKolledge_Leib.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Department).AsNoTracking();
            return View(courses);
        }

        // Create //
        [HttpGet]
        public IActionResult Create() 
        {
            PopulateDepartmentsDropDownList();
            ViewData["ViewType"] = "Create";
            return View("CreateEdit");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            PopulateDepartmentsDropDownList();
            ViewData["ViewType"] = "Edit";
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View("CreateEdit", course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("CourseId,Title,Credits,DepartmentId")] Course course, string ViewType)
        {
            if (ModelState.IsValid)
            {
                if (ViewType == "Edit")
                {
                    _context.Update(course);

                }
                else
                {
                    _context.Add(course);
                }
                await _context.SaveChangesAsync();
            }
            if (ViewType == "Edit")
            {
                return View(course);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // Delete //
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["ViewType"] = "Delete";
            if (id == null || _context.Courses == null) 
            {
                return NotFound();
            }
            var courses = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (courses == null) 
            {
                return NotFound();
            }
            return View("DeleteDetails", courses);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["ViewType"] = "Details";
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View("DeleteDetails", course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Populate //
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentQuery = from d in _context.Departments 
                                  orderby d.Name 
                                  select d;
            ViewBag.DepartmentId = new SelectList(departmentQuery.AsNoTracking(), "DepartmentId", "Name", selectedDepartment);
        }

    }
}
