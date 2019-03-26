using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class SubjectFormViewModel
    {
        public IEnumerable<Class> Classes { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        public Subject Subject { get; set; }
        public string Title
        {
            get
            {
                if (Subject != null && Subject.SubjectId != 0)
                    return "Edit Student";

                return "New Student";
            }
        }
    }
}