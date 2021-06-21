using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult AddNewUser()
        {
            return View();
        }
    }
}