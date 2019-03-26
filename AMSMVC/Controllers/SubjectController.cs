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
    public class SubjectController : Controller
    {
        // GET: Subject
        private ApplicationDbContext _context;

        public SubjectController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var classes = _context.Classes.ToList();
            var departments = _context.Departments.ToList();
            var subjectViewModel = new SubjectFormViewModel()
            {
                Classes = classes,
                Departments = departments
            };
            return View("SubjectForm", subjectViewModel);
        }

        public ActionResult Edit(int id)
        {
            var subject = _context.Subjects.SingleOrDefault(c => c.SubjectId == id);
            if (subject == null)
                return HttpNotFound();
            var subjectViewModel = new SubjectFormViewModel()
            {
                Subject = subject,
                Classes = _context.Classes.ToList(),
                Departments = _context.Departments.ToList()
            };
            return View("SubjectForm", subjectViewModel);
        }

        public ActionResult Delete(int id)
        {
            var subjectInDb = _context.Subjects.SingleOrDefault(c => c.SubjectId == id);
            if (subjectInDb == null)
                return HttpNotFound();
            _context.Subjects.Remove(subjectInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Subject");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Subject subject)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("SubjectForm");
            //}
            var checkSubjectInDb = _context.Subjects.SingleOrDefault(s => s.ClassId == subject.ClassId && s.SubjectName == subject.SubjectName);
            if (subject.SubjectId == 0)
            {
                if (checkSubjectInDb != null)
                {
                    return RedirectToAction("Index", "Subject");
                }

                var countSubjectInDb = _context.Subjects.Where(s => s.ClassId == subject.ClassId).Count();
                if (countSubjectInDb >= _context.Classes.SingleOrDefault(c => c.ClassId == subject.ClassId).MaxSubject)
                    return RedirectToAction("Index", "Subject");

                _context.Subjects.Add(subject);
            }
            else
            {
                var subjectInDb = _context.Subjects.Single(c => c.SubjectId == subject.SubjectId);
                subjectInDb.SubjectId = subject.SubjectId;
                subjectInDb.SubjectCode = subject.SubjectCode;
                subjectInDb.DepartmentId = subject.DepartmentId;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Subject");
        }

        public ViewResult Index()
        {
            var subjects = _context.Subjects.Include(c => c.Class).Include(d => d.Department).ToList();
            return View(subjects);
        }

        public ActionResult Details(int id) 
        {
            var subject = _context.Subjects.Include(c => c.Class).Include(d => d.Department).SingleOrDefault(c => c.SubjectId == id);
            if (subject == null)
                return HttpNotFound();
            return View(subject);
        }
    }
}