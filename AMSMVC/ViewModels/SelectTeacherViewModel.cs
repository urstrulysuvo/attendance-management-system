using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class SelectTeacherViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public Teacher Teacher { get; set; }
    }
}