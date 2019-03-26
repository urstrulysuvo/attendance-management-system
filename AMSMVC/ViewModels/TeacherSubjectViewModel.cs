using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace AMSMVC.ViewModels
{
    public class TeacherSubjectViewModel
    {
        public Teacher Teacher { get; set; }
        public List<Subject> Subjects { get; set; }

        public Subject Subject { get; set; }
    }
}