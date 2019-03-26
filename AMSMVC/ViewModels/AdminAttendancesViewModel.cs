using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMSMVC.Models;

namespace AMSMVC.ViewModels
{
    public class AdminAttendancesViewModel
    {
        public Class ClassInfo { get; set; }
        public List<AttendanceView> AttendanceViews { get; set; }
        public List<Subject> Subjects { get; set; }
        public int[] TotalAttendanceForSubject { get; set; }
    }
}