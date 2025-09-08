using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledge_Leib.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }

        /* My own stuff */

        public string? StudentGroup { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
