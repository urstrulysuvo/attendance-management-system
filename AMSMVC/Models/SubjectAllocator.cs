using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class SubjectAllocator
    {
        public int Id { get; set; }

        public Teacher Teacher { get; set; }
        [Required]
        [Display(Name = "Teacher Name")]
        public int TeacherId { get; set; }

        [Required]
        [Display(Name = "Subjects 1")]
        public int SubjectId1 { get; set; }

        [Required]
        [Display(Name = "Subjects 2")]
        public int SubjectId2 { get; set; }

        [Required]
        [Display(Name = "Subjects 3")]
        public int SubjectId3 { get; set; }

        [Display(Name = "Subjects Temporary")]
        public int? SubjectId4 { get; set; }
    }
}