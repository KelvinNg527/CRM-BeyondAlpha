using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.LoginProcessor;
    public class HomeController : Controller
    {
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID.StartsWith("A"))
                {
                    var adminDetails = AdminLogin(model.ID, model.Password);
                    if (adminDetails != null)
                    {
                        Session["position"] = adminDetails.Position;

                        Session["user"] = new LoginModel(adminDetails.ID, adminDetails.Password,
                            adminDetails.Name, adminDetails.Position);
                        return RedirectToAction("index", "Home");
                    }
                }
                
            }
            ViewBag.MessageError = "Username or Password is wrong!";
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        
    }
}