using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class SubjectAllocatorViewModel
    {
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public SubjectAllocator SubjectAllocator { get; set; }
    }
}