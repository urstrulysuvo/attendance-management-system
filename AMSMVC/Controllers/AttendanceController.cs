using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AMSMVC.Models;
using AMSMVC.ViewModels;

namespace AMSMVC.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        private ApplicationDbContext _context;

        public AttendanceController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = "StaffRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select(TeacherSubjectViewModel teacherSubject)
        {
            var teacher = _context.Teachers.Single(t => t.TeacherId == teacherSubject.Teacher.TeacherId);
            var subject = _context.Subjects.Single(s => s.SubjectId == teacherSubject.Subject.SubjectId);
            var students = _context.Students.Where(s => s.ClassId == subject.ClassId).OrderBy(s => s.RollNo).ToList();
            var attendances = new List<AttendanceInDetails>();
            int checkAttendanceInDb = _context.Attendances.Where(a => DbFunctions.TruncateTime(a.AttandanceDate) == DbFunctions.TruncateTime(DateTime.Now) && a.SubjectId == subject.SubjectId).ToList().Count;
            if (checkAttendanceInDb == 0)
            {
                foreach (var student in students)
                {
                    var attendance = new AttendanceInDetails()
                    {
                        RollNo = student.RollNo,
                        StudentName = student.StudentName,
                        StudentId = student.StudentId,
                        SubjectId = subject.SubjectId,
                    };
                    attendances.Add(attendance);
                }
            }
            return View("AttendanceForm", attendances);
        }

        [Authorize(Roles = "StaffRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(List<AttendanceInDetails> attendancesFromView)
        {
            var attendanceDb = _context.Attendances;
            var dateTime = DateTime.Now;
            for (int i = 0; i < attendancesFromView.Count; i++)
            {
                var attendance = new Attendance()
                {
                    AttandanceDate = dateTime,
                    StudentId = attendancesFromView[i].StudentId,
                    SubjectId = attendancesFromView[i].SubjectId,
                    Status = attendancesFromView[i].Status
                };
                attendanceDb.Add(attendance);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Attendance");
        }

        public ActionResult Index()
        {
            if (User.IsInRole("AdminRole"))
            {
                var selectClasses = new SelectClassViewModel()
                {
                    Classes = _context.Classes.ToList()
                };
                return View("SelectClass", selectClasses);
            }

            if (User.IsInRole("StaffRole"))
            {
                var teacher = _context.Teachers.Single(t => t.TeacherEmailId == User.Identity.Name);
                var subjectAllocator = _context.SubjectAllocators.SingleOrDefault(sa => sa.TeacherId == teacher.TeacherId);
                var subjects = new List<Subject>();
                subjects.Add(_context.Subjects.SingleOrDefault(s => s.SubjectId == subjectAllocator.SubjectId1));
                if (subjectAllocator.SubjectId2 != subjectAllocator.SubjectId1)
                    subjects.Add(_context.Subjects.SingleOrDefault(s => s.SubjectId == subjectAllocator.SubjectId2));
                if (subjectAllocator.SubjectId3 != subjectAllocator.SubjectId1 && subjectAllocator.SubjectId3 != subjectAllocator.SubjectId2)
                    subjects.Add(_context.Subjects.SingleOrDefault(s => s.SubjectId == subjectAllocator.SubjectId3));
                if (subjectAllocator.SubjectId4 != null)
                    if (subjectAllocator.SubjectId4 != subjectAllocator.SubjectId1 && subjectAllocator.SubjectId4 != subjectAllocator.SubjectId2 && subjectAllocator.SubjectId4 != subjectAllocator.SubjectId3)
                        subjects.Add(_context.Subjects.SingleOrDefault(s => s.SubjectId == subjectAllocator.SubjectId4));
                var teacherSubjectsViewModel = new TeacherSubjectViewModel()
                {
                    Teacher = teacher,
                    Subjects = subjects
                };
                return View(teacherSubjectsViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "AdminRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewAttendaces(SelectClassViewModel selectedClassVM)
        {
            var classInDb = _context.Classes.SingleOrDefault(c => c.ClassId == selectedClassVM.SelectedClass.ClassId);
            var studentsInDb = _context.Students.Where(st => st.ClassId == classInDb.ClassId).OrderBy(st => st.RollNo).ToList();
            var subjectsForClass = _context.Subjects.Where(s => s.ClassId == classInDb.ClassId).OrderBy(s => s.SubjectId).ToList();
            var attendanceInDb = _context.Attendances.ToList();
            var attendanceViews = new List<AttendanceView>();
            int[] totalAttendanceForSubject = new int[classInDb.MaxSubject];
            for(int i = 0; i < subjectsForClass.Count; i++)
            {
                totalAttendanceForSubject[i] = attendanceInDb.Where(a => a.StudentId == studentsInDb[0].StudentId && a.SubjectId == subjectsForClass[i].SubjectId).ToList().Count;
            }
            foreach (Student stud in studentsInDb)
            {
                int[] attendanceCount = new int[classInDb.MaxSubject];
                for(int i = 0; i < subjectsForClass.Count; i++)
                {
                    attendanceCount[i] = attendanceInDb.Where(a => a.StudentId == stud.StudentId && a.SubjectId == subjectsForClass[i].SubjectId && a.Status == true).ToList().Count;
                }
                var attendanceViewCreate = new AttendanceView()
                {
                    Student = stud,
                    AttendanceCount = attendanceCount
                };
                attendanceViews.Add(attendanceViewCreate);
            }
            var adAttendanceVM = new AdminAttendancesViewModel()
            {
                ClassInfo = classInDb,
                TotalAttendanceForSubject = totalAttendanceForSubject,
                Subjects = subjectsForClass,
                AttendanceViews = attendanceViews
            };
            return View("AttendanceReport", adAttendanceVM);
        }
    }
}