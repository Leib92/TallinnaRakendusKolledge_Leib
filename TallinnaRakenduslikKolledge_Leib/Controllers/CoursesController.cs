using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledge_Leib.Data;

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
        [HttpGet]
        public IActionResult Create() 
        {
            PopulateDepartmentsDropDownList();
            return View();
        }
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentQuery = from d in _context.Departments 
                                  orderby d.Name 
                                  select d;
            ViewBag.DepartmentId = new SelectList(departmentQuery.AsNoTracking(), "DepartmentId", "Name", selectedDepartment);
        }
    }
}
