using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledge_Leib.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Eesnimi")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Perekonnanimi")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Õpetaja nimi")]
        public string FullName 
        {
            get { return FirstName + ", " + LastName; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Tööleasumiskuupäev")]
        public DateTime HireDate { get; set; }

        public ICollection<CourseAssignment>? CourseAssignments { get; set; }
        public OfficeAssignment? OfficeAssignment { get; set; }

        /* Optional */
        
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Display(Name = "Telefoni number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Palk")]
        public int Salary { get; set; }

    }
}
