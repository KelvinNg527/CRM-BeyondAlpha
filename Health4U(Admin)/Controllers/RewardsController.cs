using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    public class RewardsController : Controller
    {
        // GET: Rewards
        public ActionResult Rewards()
        {
            return View();
        }

        public ActionResult AddRewards()
        {
            return View();
        }
        public ActionResult AddBooking()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult BDetails()
        {
            return View();
        }
        public ActionResult Booking()
        {
            return View();
        }
        public ActionResult PendingBooking()
        {
            return View();
        }
        public ActionResult PBDetails()
        {
            return View();
        }
    }
}