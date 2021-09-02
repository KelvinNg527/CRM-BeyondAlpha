using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    public class DiseaseController : Controller
    {
        // GET: Disease
        public ActionResult Disease()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}