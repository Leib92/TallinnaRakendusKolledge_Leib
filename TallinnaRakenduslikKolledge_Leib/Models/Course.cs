using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledge_Leib.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<CourseAssignment>? CourseAssignments { get; set; }
    }
}
