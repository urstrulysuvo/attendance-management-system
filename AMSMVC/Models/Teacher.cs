using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Please enter teacher's name")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 50 characters")]
        [Display(Name = "Teacher Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$", ErrorMessage = "Name should not contain any number or special character")]
        public string TeacherName { get; set; }

        public Department Department { get; set; }
        [Required(ErrorMessage = "Please select teacher's department")]
        [Display(Name = "Department")]
        public Byte DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter teacher's email id")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string TeacherEmailId { get; set; }
    }
}