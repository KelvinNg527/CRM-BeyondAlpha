using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    public class BlacklistController : Controller
    {
        // GET: Blacklist
        public ActionResult Blacklist()
        {
            return View();
        }

        // GET: Blacklist
        public ActionResult AddBlacklist()
        {
            return View();
        }
    }
}