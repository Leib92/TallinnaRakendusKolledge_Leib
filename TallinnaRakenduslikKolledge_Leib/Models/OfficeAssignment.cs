using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledge_Leib.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        [StringLength(50)]
        [Display(Name = "Kabinet")]
        public string Location { get; set; }
    }
}
