using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class AttendanceView
    {
        public Student Student { get; set; }
        public int[] AttendanceCount { get; set; }
    }
}