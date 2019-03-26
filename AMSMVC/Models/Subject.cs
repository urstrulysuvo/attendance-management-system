using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Please enter subject code")]
        [MaxLength(6),MinLength(5)]
        [Display(Name = "Subject Code")]
        public string SubjectCode { get; set; }

        [Required(ErrorMessage = "Please enter subject name")]
        [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 100 characters")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        public Department Department { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public Byte DepartmentId { get; set; }

        public Class Class { get; set; }
        [Required(ErrorMessage = "Please select subject for the class")]
        [Display(Name = "For Class")]
        public Byte ClassId { get; set; }
    }
}