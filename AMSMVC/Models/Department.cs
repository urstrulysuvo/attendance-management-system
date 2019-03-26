using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class Department
    {
        [Display(Name = "Department Id")]
        public Byte DepartmentId { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Teacher Intake")]
        public int MaxTeacher { get; set; }
    }
}