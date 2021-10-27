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

        public ActionResult AddRewards()
        {
            var recordsBloodType = LoadBloodType().ToList();
            SelectList BloodType = new SelectList(recordsBloodType, "BloodTypeID", "BloodTypeCb");
            ViewBag.BloodType = BloodType;

            return View();
        }

        [HttpPost]
        public ActionResult AddRewards(Rewards model)
        {
            List<Rewards> Bill = new List<Rewards>();
            Rewards app = new Rewards();

            var data = LoadRewards();

            if (model.IsGovernment == true)
            {
                var lastGRewardsID = data.AsQueryable().OrderByDescending(c => c.RewardID).Where(x => x.IsGovernment == true).FirstOrDefault();

                if (lastGRewardsID == null)
                {
                    app.RewardID = "G001";
                }
                else
                {
                    app.RewardID = "G" + (Convert.ToInt32(lastGRewardsID.RewardID.Substring
                        (1, lastGRewardsID.RewardID.Length - 1)) + 1).ToString("D3");
                }
                int recordsCreated = CreateReward(app.RewardID,
             model.Description,model.IsGovernment,model.Note,model.BloodTypeID,
             model.MinRequirement, model.PhysicalLocation,
              model.MaxClaim);
            }
            else
            {
                var lastPRewardsID = data.AsQueryable().OrderByDescending(c => c.RewardID).Where(x => x.IsGovernment == false).FirstOrDefault();
                if (lastPRewardsID == null)
                {
                    app.RewardID = "P001";
                }
                else
                {
                    app.RewardID = "P" + (Convert.ToInt32(lastPRewardsID.RewardID.Substring
                        (1, lastPRewardsID.RewardID.Length - 1)) + 1).ToString("D3");
                }
                int recordsCreated = CreateReward(app.RewardID,
             model.Description, model.IsGovernment, model.Note, model.BloodTypeID,
             model.MinRequirement, model.PhysicalLocation,
              model.MaxClaim);
            }


          
            return RedirectToAction("ViewRewards");

        }
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

     

        [HttpGet]
        public ActionResult RDetails(string id)
        {
            if (ModelState.IsValid)
            {

                var recordsSelected = SelectRewards(id);
                var recordsBloodType = LoadBloodType().ToList();
                SelectList BloodType = new SelectList(recordsBloodType, "BloodTypeID", "BloodTypeCb");
                ViewBag.BloodType = BloodType;
 

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
                 model.MinRequirement,model.MaxClaim,model.PhysicalLocation);
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

        public ActionResult ViewClaimRecords()
        {
            List<claim_rewards_records> Rewards = new List<claim_rewards_records>();
            var data = LoadClaimedRewards();

            foreach (var row in data)
            {
                Rewards.Add(new claim_rewards_records
                {
                    UserID=row.UserID,
                    AppointmentID=row.AppointmentID,
                    CompletionTime=row.CompletionTime,
                    RewardID=row.RewardID,
                    ClaimTime=row.ClaimTime
                });
            }

            return View(Rewards);
        }

    }
}