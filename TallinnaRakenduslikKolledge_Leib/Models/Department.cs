using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledge_Leib.Models
{
    public enum DepartmentRating
    {
        Excellent,Good,Acceptable,Deterorating,FUBAR
    }
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Administrator { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public byte? RowVersion { get; set; }

        /* 3 lisa */
        public int? TeachersKilled { get; set; }
        public string? TragicBackstory { get; set; }
        public DepartmentRating? CurrentRating { get; set; } 

    }
}
