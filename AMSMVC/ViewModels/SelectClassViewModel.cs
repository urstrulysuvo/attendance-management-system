using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class SelectClassViewModel
    {
        public IEnumerable<Class> Classes { get; set; }
        public Class SelectedClass { get; set; }
    }
}