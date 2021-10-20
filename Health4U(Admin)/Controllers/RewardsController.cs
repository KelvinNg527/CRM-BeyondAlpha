using System;
using DataLibrary.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.RewardsProcessor;

    public class RewardsController : Controller
    {
        // GET: Rewards
        public ActionResult ViewRewards()
        {
        List<Rewards> Rewards = new List<Rewards>();
            var data = LoadRewards();

            foreach (var row in data)
            {
                Rewards.Add(new Rewards
                {
                    RewardID = row.RewardID,
                    IsActive = row.IsActive,
                    Description = row.Description,
                    IsGovernment = row.IsGovernment,
                    Note = row.Note,
                    MinRequirement = row.MinRequirement,
                    PhysicalLocation = row.PhysicalLocation,
                    MaxClaim = row.MaxClaim,
                    BloodTypeID = row.BloodTypeID,
                    BloodType=row.BloodType,
                    BloodVolume=row.BloodVolume
         
                });
            }

            return View(Rewards);
        }

        public ActionResult AddRewards()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RDetails(string id)
        {
            if (ModelState.IsValid)
            {

                var recordsSelected = SelectRewards(id);
                var recordsBloodType = LoadBloodType().ToList();
                var recordsIDSelected = LoadIsActive();
                SelectList BloodType = new SelectList(recordsBloodType, "BloodTypeID", "BloodTypeCb");
                ViewBag.BloodType = BloodType;
                SelectList isActive = new SelectList(recordsIDSelected, "IsActive", "IsActive");
                ViewBag.isActive = isActive;

                var model = new Rewards()
                {
                    RewardID=recordsSelected.RewardID,
                    IsActive=recordsSelected.IsActive,
                    Description=recordsSelected.Description,
                    IsGovernment=recordsSelected.IsGovernment,
                    Note=recordsSelected.Note,
                    BloodType=recordsSelected.BloodType,
                    MinRequirement=recordsSelected.MinRequirement,
                    PhysicalLocation=recordsSelected.PhysicalLocation,
                    MaxClaim=recordsSelected.MaxClaim
                };


                return View(model);
            }

            return RedirectToAction("ViewRewards");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RDetails(Rewards model)
        {

            if (model != null)
            {
                int recordsUpdated =
                    UpdateRewards(model.RewardID,
                 model.IsActive,
                 model.Description, model.Note, model.BloodTypeID,
                 model.MinRequirement,model.MaxClaim);
                return RedirectToAction("ViewRewards");
            }
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult DeleteReward(string id)
        {

            if (ModelState.IsValid)
            {

                var recordsDelete = DeleteRewards(id);

                return RedirectToAction("ViewRewards");
            }
            return RedirectToAction("ViewRewards");
        }

    }
}