using AMSMVC.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMSMVC.ViewModels;

namespace AMSMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        private ApplicationDbContext _context;

        public StudentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = "AdminRole")]
        public ActionResult New()
        {
            var classes = _context.Classes.ToList();
            var studentViewModel = new StudentFormViewModel()
            {
                Classes = classes
            };
            return View("StudentForm", studentViewModel);
        }

        [Authorize(Roles="AdminRole")]
        public ActionResult Edit(int id)
        {
            var student = _context.Students.SingleOrDefault(c => c.StudentId == id);
            if (student == null)
                return HttpNotFound();
            var studentViewModel = new StudentFormViewModel()
            {
                Student = student,
                Classes = _context.Classes.ToList()
            };

            return View("StudentForm", studentViewModel);
        }

        [Authorize(Roles = "AdminRole")]
        public ActionResult Delete(int id)
        {
            var studentInDb = _context.Students.SingleOrDefault(c => c.StudentId == id);
            if (studentInDb == null)
                return HttpNotFound();
            _context.Students.Remove(studentInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Student");
        }

        [Authorize(Roles = "AdminRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Student student)
        {
            //if(!ModelState.IsValid)
            //{
            //    var studentViewModel = new StudentFormViewModel
            //    {
            //        Student = student,
            //        Departments = _context.Departments.ToList()
            //    };
            //    return View("StudentForm", studentViewModel);
            //}
            
            var checkStudentInDb = _context.Students.SingleOrDefault(s => s.ClassId == student.ClassId && s.RollNo == student.RollNo);
            
            if(student.StudentId == 0)
            {
                if(checkStudentInDb != null)
                {
                    return RedirectToAction("Index", "Student");
                }

                var countStudentInDb = _context.Students.Where(s => s.ClassId == student.ClassId).Count();
                if (countStudentInDb >= _context.Classes.SingleOrDefault(c => c.ClassId == student.ClassId).MaxStudent)
                    return RedirectToAction("Index", "Student");

                _context.Students.Add(student);
            }
            else
            {
                var studentInDb = _context.Students.Single(c => c.StudentId == student.StudentId);
                studentInDb.StudentId = student.StudentId;
                studentInDb.StudentName = student.StudentName;
                studentInDb.StudentEmailId = student.StudentEmailId;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Student");

        }

        public ActionResult Index()
        {
            var students = _context.Students.Include(c => c.Class).ToList();
            if (User.IsInRole("AdminRole"))
                return View("Index", students);
            if (User.IsInRole("StaffRole"))
            {
                var teacher = _context.Teachers.Single(t => t.TeacherEmailId == User.Identity.Name);
                var subjectAllocator = _context.SubjectAllocators.SingleOrDefault(sa => sa.TeacherId == teacher.TeacherId);
                var subject1 = _context.Subjects.Single(s => s.SubjectId == subjectAllocator.SubjectId1);
                var subject2 = _context.Subjects.Single(s => s.SubjectId == subjectAllocator.SubjectId2);
                var subject3 = _context.Subjects.Single(s => s.SubjectId == subjectAllocator.SubjectId3);
                var subject4 = subject1;
                if(subjectAllocator.SubjectId4 != null)
                    subject4 = _context.Subjects.Single(s => s.SubjectId == subjectAllocator.SubjectId4);
                var studentsFilter = _context.Students.Where(s => s.ClassId == subject1.ClassId || s.ClassId == subject2.ClassId || s.ClassId == subject3.ClassId || s.ClassId == subject4.ClassId).ToList();
                return View("ReadOnly", studentsFilter);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            var student = _context.Students.Include(c => c.Class).SingleOrDefault(c => c.StudentId == id);
            if (student == null)
                return HttpNotFound();
            return View(student);
        }
    }
}