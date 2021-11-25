using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Report()
        {
            var Report = "";
            Report = TempData["Report"] as string;
            if (Report == "1")
            {
                ViewBag.Report = "1";
            }
            else
            {
                ViewBag.Report = "2";
            }

            return View();
        }


        public ActionResult ViewReport(string Report)
        {
            TempData["Report"] = Report;


            return RedirectToAction("Report");
      }
    }
}