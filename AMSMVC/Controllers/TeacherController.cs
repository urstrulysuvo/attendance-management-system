using AMSMVC.Models;
using AMSMVC.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMSMVC.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class TeacherController : Controller
    {
        // GET: Teacher
        private ApplicationDbContext _context;

        public TeacherController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var departments = _context.Departments.ToList();
            var teacherViewModel = new TeacherFormViewModel()
            {
                Departments = departments
            };
            return View("TeacherForm", teacherViewModel);
        }

        public ActionResult Edit(int id)
        {
            var teacher = _context.Teachers.SingleOrDefault(c => c.TeacherId == id);
            if (teacher == null)
                return HttpNotFound();
            var teacherViewModel = new TeacherFormViewModel()
            {
                Teacher = teacher,
                Departments = _context.Departments.ToList()
            };

            return View("TeacherForm", teacherViewModel);
        }

        public ActionResult Delete(int id)
        {
            var teacherInDb = _context.Teachers.SingleOrDefault(c => c.TeacherId == id);
            if (teacherInDb == null)
                return HttpNotFound();
            _context.Teachers.Remove(teacherInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Teacher");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Teacher teacher)
        {
            //if (!ModelState.IsValid)
            //{
            //    var teacherViewModel = new TeacherFormViewModel
            //    {
            //        Teacher = teacher,
            //        Departments = _context.Departments.ToList()
            //    };
            //    return View("TeacherForm", teacherViewModel);
            //}
            
            var checkTeacherInDb = _context.Teachers.SingleOrDefault(s => s.DepartmentId == teacher.DepartmentId && s.TeacherEmailId == teacher.TeacherEmailId);

            if (teacher.TeacherId == 0)
            {
                if (checkTeacherInDb != null)
                {
                    return RedirectToAction("Index", "Teacher");
                }

                var countTeacherInDb = _context.Teachers.Where(s => s.DepartmentId == teacher.DepartmentId).Count();
                if (countTeacherInDb >= _context.Departments.SingleOrDefault(c => c.DepartmentId == teacher.DepartmentId).MaxTeacher)
                    return RedirectToAction("Index", "Teacher");

                _context.Teachers.Add(teacher);
            }
            else
            {
                var teacherInDb = _context.Teachers.Single(c => c.TeacherId == teacher.TeacherId);
                teacherInDb.TeacherId = teacher.TeacherId;
                teacherInDb.TeacherName = teacher.TeacherName;
                teacherInDb.DepartmentId = teacher.DepartmentId;
                teacherInDb.TeacherEmailId = teacher.TeacherEmailId;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Teacher");
        }

        public ViewResult Index()
        {
            var teachers = _context.Teachers.Include(c => c.Department).ToList();
            return View(teachers);
        }

        public ActionResult Details(int id) 
        {
            var teacher = _context.Teachers.Include(c => c.Department).SingleOrDefault(c => c.TeacherId == id);
            if (teacher == null)
                return HttpNotFound();
            return View(teacher);
        }
    }
}