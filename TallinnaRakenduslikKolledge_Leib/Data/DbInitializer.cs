using System.Reflection;
using TallinnaRakenduslikKolledge_Leib.Models;

namespace TallinnaRakenduslikKolledge_Leib.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student
                {
                    FirstName = "George",
                    LastName = "Teemus",
                    EnrollmentDate = DateTime.Now,

                },
                new Student
                {
                    FirstName = "Tony",
                    LastName = "Rengi",
                    EnrollmentDate = DateTime.Now,

                },
                new Student
                {
                    FirstName = "John",
                    LastName = "Johnson",
                    EnrollmentDate = DateTime.Now,

                },
                new Student
                {
                    FirstName = "Robert",
                    LastName = "Stonewood",
                    EnrollmentDate = DateTime.Now,

                },
                new Student
                {
                    FirstName = "Billy",
                    LastName = "Wolkin",
                    EnrollmentDate = DateTime.Now,

                }
            };
            context.Students.AddRange(students);
            context.SaveChanges();
            //
            if (context.Students.Any()) { return; }
            var courses = new Course[]
            {
                new Course {CourseId=1001, Title="Programmeerimise Alused", Credits=3},
                new Course {CourseId=2002, Title="Programmeerimise 1", Credits=3},
                new Course {CourseId=3003, Title="Programmeerimise 2", Credits=3},
                new Course {CourseId=2003, Title="Tarkvara Arendusprotsess", Credits=3},
                new Course {CourseId=1002, Title="Multimeedia", Credits=3},
                new Course {CourseId=3001, Title="Hajusrakenduste Alused", Credits=3}
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();
            //
            if (context.Enrollments.Any()) { return; }
            var enrollments = new Enrollment[]
            {
                new Enrollment {StudentID=1,CourseID=3003,CurrentGrade=Grade.X},
                new Enrollment {StudentID=1,CourseID=3001,CurrentGrade=Grade.B},
                new Enrollment {StudentID=2,CourseID=1001,CurrentGrade=Grade.A},
                new Enrollment {StudentID=2,CourseID=1002,CurrentGrade=Grade.MA},
                new Enrollment {StudentID=3,CourseID=1001,CurrentGrade=Grade.C},
                new Enrollment {StudentID=3,CourseID=2003,CurrentGrade=Grade.C},
                new Enrollment {StudentID=4,CourseID=2002,CurrentGrade=Grade.D},
                new Enrollment {StudentID=4,CourseID=3001,CurrentGrade=Grade.F}
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
            //
            if (context.Instructors.Any()) { return; }
            var instructors = new Instructor[]
            {
                new Instructor { FirstName="Vasili", LastName="Koslov", HireDate=DateTime.Now, Salary=2000 },
                new Instructor { FirstName="Boris", LastName="Vljedlev", HireDate=DateTime.Now, Salary=3000 },
                new Instructor { FirstName="Mark", LastName="Rubic", HireDate=DateTime.Now, Salary=2500 }
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();
        }
    }
}
