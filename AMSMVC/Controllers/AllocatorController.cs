using AMSMVC.Models;
using AMSMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMSMVC.Controllers
{
    public class AllocatorController : Controller
    {
        // GET: Allocator
        private ApplicationDbContext _context;

        public AllocatorController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var teachers = _context.Teachers.ToList();
            var selectTeacherViewModel = new SelectTeacherViewModel()
            {
                Teachers = teachers
            };
            return View("SelectTeacher", selectTeacherViewModel);
        }

        public ActionResult Select(Teacher teacher)
        {
            var teacherInDb = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacher.TeacherId);
            var subjectAllocator = new SubjectAllocator()
            {
                TeacherId = teacherInDb.TeacherId
            };
            var subjects = _context.Subjects.Where(s => s.DepartmentId == teacherInDb.DepartmentId).ToList();
            var subjectAllocViewModel = new SubjectAllocatorViewModel()
            {
                SubjectAllocator = subjectAllocator,
                Subjects = subjects
            };

            return View("AllocationForm", subjectAllocViewModel);
        }

        public ActionResult Edit(int id)
        {
            var subjectAllocInDb = _context.SubjectAllocators.SingleOrDefault(c => c.Id == id);
            if (subjectAllocInDb == null)
                return HttpNotFound();

            var teacherInDb = _context.Teachers.SingleOrDefault(t => t.TeacherId == subjectAllocInDb.TeacherId);
            var subjects = _context.Subjects.Where(s => s.DepartmentId == teacherInDb.DepartmentId).ToList();
            var subjectAllocViewModel = new SubjectAllocatorViewModel()
            {
                SubjectAllocator = subjectAllocInDb,
                Subjects = subjects
            };

            return View("UpdateAllocForm", subjectAllocViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SubjectAllocator subjectAllocator)
        {
            var checkTeacherInDb = _context.SubjectAllocators.SingleOrDefault(t => t.TeacherId == subjectAllocator.TeacherId);
            if (subjectAllocator.Id == 0 && checkTeacherInDb == null)
            {
                _context.SubjectAllocators.Add(subjectAllocator);
            }
            else
            {
                var getSubAllocation = _context.SubjectAllocators.Single(t => t.TeacherId == subjectAllocator.TeacherId);
                int getId = getSubAllocation.Id;
                var subjectAllocInDb = _context.SubjectAllocators.Single(c => c.Id == getId);
                subjectAllocInDb.TeacherId = subjectAllocator.TeacherId;
                subjectAllocInDb.SubjectId1 = subjectAllocator.SubjectId1;
                subjectAllocInDb.SubjectId2 = subjectAllocator.SubjectId2;
                subjectAllocInDb.SubjectId3 = subjectAllocator.SubjectId3;
                subjectAllocInDb.SubjectId4 = subjectAllocator.SubjectId4;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Allocator");

        }

        public ViewResult Index()
        {
            var subjectAllocator = _context.SubjectAllocators.ToList();
            var subjects = _context.Subjects.ToList();
            var teachers = _context.Teachers.ToList();
            var subAllocTableViewModel = new List<SubjectAllocatorTableViewModel>();
            foreach (SubjectAllocator sa in subjectAllocator)
            {
                var subject = new Subject[4];
                var teacher = teachers.SingleOrDefault(t => t.TeacherId == sa.TeacherId);
                subject[0] = subjects.SingleOrDefault(s => s.SubjectId == sa.SubjectId1);
                subject[1] = subjects.SingleOrDefault(s => s.SubjectId == sa.SubjectId2);
                subject[2] = subjects.SingleOrDefault(s => s.SubjectId == sa.SubjectId3);
                subject[3] = subjects.SingleOrDefault(s => s.SubjectId == sa.SubjectId4);
                
                var sAllocTable = new SubjectAllocatorTableViewModel()
                {
                    SubjectAllocator = sa,
                    Teacher = teacher,
                    Subject = subject
                };
                subAllocTableViewModel.Add(sAllocTable);
            }
            return View(subAllocTableViewModel);
        }
    }
}