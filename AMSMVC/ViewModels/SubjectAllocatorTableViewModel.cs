using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class SubjectAllocatorTableViewModel
    {
        public SubjectAllocator SubjectAllocator { get; set; }
        public Subject[] Subject { get; set; }
        public Teacher Teacher { get; set; }
    }
}