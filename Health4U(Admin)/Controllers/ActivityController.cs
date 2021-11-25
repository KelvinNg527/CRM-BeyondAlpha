using System;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.ActivityProcessor;
    public class ActivityController : Controller
    {
        public ActionResult PendingActivity()
        {
            var user = Session["user"] as LoginModel;
            if (user == null) { return RedirectToAction("login", "Home"); }
            List<ActivityModel> pending = new List<ActivityModel>();
            var data = LoadPendingActivity();

            foreach (var row in data)
            {
                pending.Add(new ActivityModel
                {
                    EventID = row.EventID,
                    EventStartDate = row.EventStartDate,
                    EventEndDate = row.EventEndDate,
                    Venue = row.Venue,
                    Country = row.Country
                });
            }
            return View(pending);
        }

        [HttpGet]
        public ActionResult Details(int EventID)
        {
            var user = Session["user"] as LoginModel;
            if (user == null) { return RedirectToAction("login", "Home"); }
            if (ModelState.IsValid)
            {
                var recordSelected = SelectEventInfor(EventID);

                var model = new ActivityModel()
                {
                    EventID = recordSelected.EventID,
                    EventStartDate = recordSelected.EventStartDate,
                    EventEndDate = recordSelected.EventEndDate,
                    Name = recordSelected.Name,
                    Venue = recordSelected.Venue,
                    Country = recordSelected.Country
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Details(ActivityModel model, string selection)
        {
            var user = Session["user"] as LoginModel;
            if (user == null) { return RedirectToAction("login", "Home"); }
            if (ModelState.IsValid)
            {
                if (selection == "Y")
                {
                    if (model.Note == null)
                    {
                        int recordsUpdate = UpdateApNn(model.EventID, user.ID);
                        return RedirectToAction("PendingActivity", "Activity");
                    }
                    else
                    {
                        int recordsUpdate = UpdateApYn(model.EventID, model.Note, user.ID);
                        return RedirectToAction("PendingActivity", "Activity");
                    }
                }else if (selection == "N")
                {
                    if (model.Note == null)
                    {
                        int recordsUpdate = UpdateApFNn(model.EventID, user.ID);
                        return RedirectToAction("PendingActivity", "Activity");
                    }
                    else
                    {
                        int recordsUpdate = UpdateApFYn(model.EventID, model.Note, user.ID);
                        return RedirectToAction("PendingActivity", "Activity");
                    }
                }
            }
            ViewBag.MessageError = "Error!! Cannot Update to database. Please try again!!";
            return View();
        }

    }
}