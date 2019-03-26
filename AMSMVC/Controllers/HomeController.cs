using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMSMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("AdminRole") || User.IsInRole("StaffRole"))
                return RedirectToAction("Index", "Student");

            return View();
        }

        public ActionResult About()
        {
            if (User.IsInRole("AdminRole") || User.IsInRole("StaffRole"))
                return RedirectToAction("Index", "Student");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (User.IsInRole("AdminRole") || User.IsInRole("StaffRole"))
                return RedirectToAction("Index", "Student");

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}