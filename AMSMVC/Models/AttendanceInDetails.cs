using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMSMVC.Models
{
    public class AttendanceInDetails
    {
        public int StudentId { get; set; }

        public int RollNo { get; set; }

        public int SubjectId { get; set; }

        public String StudentName { get; set; }

        public bool Status { get; set; }
    }
}