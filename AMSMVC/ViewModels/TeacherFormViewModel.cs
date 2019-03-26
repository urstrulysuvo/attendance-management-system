using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class TeacherFormViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public Teacher Teacher { get; set; }
        public string Title
        {
            get
            {
                if (Teacher != null && Teacher.TeacherId != 0)
                    return "Edit Teacher";

                return "New Teacher";
            }
        }
    }
}