using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class Class
    {
        [Display(Name = "Class Id")]
        public Byte ClassId { get; set; }

        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Display(Name = "Student Intake")]
        public int MaxStudent { get; set; }

        [Display(Name = "Subject Intake")]
        public int MaxSubject { get; set; }
    }
}