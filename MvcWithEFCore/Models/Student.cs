using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWithEFCore.Models
{
    public class Student
    {
        public int ID { get; set; }
        [StringLength(50,MinimumLength =2),RegularExpression("^[A-Z]+[a-zA-Z]*$")]
        [Required,Display(Name ="Last Name")]
        public string LastName { get; set; }
        [StringLength(maximumLength:50,MinimumLength =5)]
        [Column("FirstName")]
        [Required,Display(Name ="First Name")]
        public string FirstMidName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstMidName},{LastName}";

        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Display(Name ="Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
