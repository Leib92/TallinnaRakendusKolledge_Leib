using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledge_Leib.Data;
using TallinnaRakenduslikKolledge_Leib.Models;

namespace TallinnaRakenduslikKolledge_Leib.Controllers
{
    public class InstructorsController : Controller
    {
        // CONTEXT //
        private readonly SchoolContext _context;
        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        // INDEX //
        public async Task<IActionResult> Index(int? id, int? courseId)
        {
            var vm = new InstructorIndexData();
            vm.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ToListAsync();
            return View(vm);
        }

        // CREATE //
        [HttpGet]
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor, string selectedCourses)
        {
            if (selectedCourses != null)
            {
                instructor.CourseAssignments = new List<CourseAssignment>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignment
                    {
                        InstructorId = instructor.Id,
                        CourseId = course
                    };
                    instructor.CourseAssignments.Add(courseToAdd);
                }
                
            }
            ModelState.Remove("selectedCourses");
            if (ModelState.IsValid) 
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // DELETE //
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POPULATE //
        private void PopulateAssignedCourseData(Instructor instructor) 
        {
            var allCourses = _context.Courses; // leiame kõik kursused
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseId));
            // valime kursused kus courseid on õpetajal olemas
            var vm = new List<AssignedCourseData>();
            foreach (var course in allCourses) 
            {
                vm.Add(new AssignedCourseData
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseId)
                });
            }
            ViewData["Courses"] = vm;
        }
    }
}
