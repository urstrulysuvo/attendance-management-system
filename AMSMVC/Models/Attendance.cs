using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class Attendance
    {
        public long AttendanceId { get; set; }

        public Student Student { get; set; }
        [Required]
        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Required]
        public DateTime AttandanceDate { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
    }
}