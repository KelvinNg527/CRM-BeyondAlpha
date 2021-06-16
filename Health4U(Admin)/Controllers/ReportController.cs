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
            return View();
        }
        public ActionResult Print()
        {
            return View();
        }
    }
}