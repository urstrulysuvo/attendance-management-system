using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class StudentFormViewModel
    {
        public IEnumerable<Class> Classes { get; set; }
        public Student Student { get; set; }
        public string Title
        {
            get
            {
                if (Student != null && Student.StudentId != 0)
                    return "Edit Student";

                return "New Student";
            }
        }
    }
}