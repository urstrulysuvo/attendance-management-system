using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please enter student's name")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 50 characters")]
        [Display(Name = "Student Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$", ErrorMessage="Name should not contain any number or special character")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please enter student's roll no")]
        [Display(Name = "Roll No")]
        [Range(1,120)]
        public int RollNo { get; set; }

        public Class Class { get; set; }
        [Required(ErrorMessage = "Please select student's class")]
        [Display(Name = "Class")]
        public Byte ClassId { get; set; }

        [Required(ErrorMessage = "Please enter student's email id")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string StudentEmailId { get; set; }
    }
}