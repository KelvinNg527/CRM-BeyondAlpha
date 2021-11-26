using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.UserProcessor;
    public class UserController : Controller
    {
        // GET: User
        public ActionResult AddNewUser()
        {
            var user = Session["user"] as LoginModel;
            if (user == null) { return RedirectToAction("login", "Home"); }
            return View();
        }

        [HttpPost]
        public ActionResult AddNewUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                List<UserModel> us = new List<UserModel>();
                UserModel app = new UserModel();
                var data = LoadFullDoctor();
                var lastDoc = data.AsQueryable().OrderByDescending(c => c.DoctorID).FirstOrDefault();
                if (lastDoc == null)
                {
                    app.DoctorID = "D001";
                }
                else
                {
                    app.DoctorID = "D" + (Convert.ToInt32(lastDoc.DoctorID.Substring
                        (1, lastDoc.DoctorID.Length - 1)) + 1).ToString("D3");
                }
                var corpoInfo = CheckCorpo(model.CorporateID);
                if (corpoInfo != null)
                {
                    int recordsCreated = CreateNewUser(app.DoctorID, model.Name, model.Gender, model.SpecificationID, model.CorporateID);
                    ViewBag.Message = "New ID is " + app.DoctorID + " and Password is abc12345!";
                    return View(model);
                }
                ViewBag.Message = "Corporate ID not founded! Please insert correct corporate id.";
                return View(model);
            }
            ViewBag.Message = "Error!!!";
            return View();
        }

        public ActionResult ViewUser()
        {
            var user = Session["user"] as LoginModel;
            if (user == null) { return RedirectToAction("login", "Home"); }
            List<UserModel> doc = new List<UserModel>();
            var data = LoadFullDoctorCor();

            foreach (var row in data)
            {
                doc.Add(new UserModel
                {
                    Name = row.Name,
                    cName = row.cName,
                    Gender = row.Gender,
                    DPassword = row.DPassword
                });
            }
            return View(doc);
        }
    }
}