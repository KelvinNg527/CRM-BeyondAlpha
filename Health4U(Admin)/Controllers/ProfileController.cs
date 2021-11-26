using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.PersonalDetailsProcessor;
    public class ProfileController : Controller
    {
        public ActionResult PersonalInformation()
        {
            var user = Session["user"] as LoginModel;
            if (user == null) { return RedirectToAction("login", "Home"); }
            if (ModelState.IsValid)
            {
                if (user.ID.StartsWith("A"))
                {
                    var recordsSelected = SelectUserA(user.ID);

                    var model = new PersonalDetailsModel()
                    {
                        AdminID = recordsSelected.AdminID,
                        Name = recordsSelected.Name,
                        Gender = recordsSelected.Gender,
                        Position = recordsSelected.Position,
                        Password = recordsSelected.Password
                    };
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult PersonalInformation(PersonalDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.newPassword)
                {
                    ViewBag.MessageError = "New Password are not same with current Password!";
                    return View();
                }
                else
                {
                    if (model.newPassword == model.confrimPassword)
                    {
                        int recordsUpdated = UpdatePasswordA(model.AdminID, model.newPassword);
                        ViewBag.Message = "Update successful!";
                        return View(model);
                    }
                    else
                    {
                        ViewBag.Message = "New Password are not same with Confrim Password!";
                        return View(model);
                    }

                }
            }
            ViewBag.Message = "Error!!";
            return View();
        }
    }
}