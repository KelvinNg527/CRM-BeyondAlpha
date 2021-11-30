//using system;
//using datalibrary.models;
//using system.collections.generic;
//using system.linq;
//using system.web;
//using system.web.mvc;

//namespace health4u_admin_.controllers
//{
//    using static datalibrary.businesslogic.taskprocessor;

//    public class rewardscontroller : controller
//    {

//        public actionresult addrewards()
//        {
//            var recordsbloodtype = loadbloodtype().tolist();
//            selectlist bloodtype = new selectlist(recordsbloodtype, "bloodtypeid", "bloodtypecb");
//            viewbag.bloodtype = bloodtype;

//            return view();
//        }

//        [httppost]
//        public actionresult addrewards(rewards model)
//        {
//            list<rewards> bill = new list<rewards>();
//            rewards app = new rewards();

//            var data = loadrewards();

//            if (model.isgovernment == true)
//            {
//                var lastgrewardsid = data.asqueryable().orderbydescending(c => c.rewardid).where(x => x.isgovernment == true).firstordefault();

//                if (lastgrewardsid == null)
//                {
//                    app.rewardid = "g001";
//                }
//                else
//                {
//                    app.rewardid = "g" + (convert.toint32(lastgrewardsid.rewardid.substring
//                        (1, lastgrewardsid.rewardid.length - 1)) + 1).tostring("d3");
//                }
//                int recordscreated = createreward(app.rewardid,
//             model.description, model.isgovernment, model.note, model.bloodtypeid,
//             model.minrequirement, model.physicallocation,
//              model.maxclaim);
//            }
//            else
//            {
//                var lastprewardsid = data.asqueryable().orderbydescending(c => c.rewardid).where(x => x.isgovernment == false).firstordefault();
//                if (lastprewardsid == null)
//                {
//                    app.rewardid = "p001";
//                }
//                else
//                {
//                    app.rewardid = "p" + (convert.toint32(lastprewardsid.rewardid.substring
//                        (1, lastprewardsid.rewardid.length - 1)) + 1).tostring("d3");
//                }
//                int recordscreated = createreward(app.rewardid,
//             model.description, model.isgovernment, model.note, model.bloodtypeid,
//             model.minrequirement, model.physicallocation,
//              model.maxclaim);
//            }



//            return redirecttoaction("viewrewards");

//        }
//        get: rewards
//        public actionresult viewrewards()
//        {
//            list<rewards> rewards = new list<rewards>();
//            var data = loadrewards();

//            foreach (var row in data)
//            {
//                rewards.add(new rewards
//                {
//                    rewardid = row.rewardid,
//                    isactive = row.isactive,
//                    description = row.description,
//                    isgovernment = row.isgovernment,
//                    note = row.note,
//                    minrequirement = row.minrequirement,
//                    physicallocation = row.physicallocation,
//                    maxclaim = row.maxclaim,
//                    bloodtypeid = row.bloodtypeid,
//                    bloodtype = row.bloodtype,
//                    bloodvolume = row.bloodvolume

//                });
//            }

//            return view(rewards);
//        }



//        [httpget]
//        public actionresult rdetails(string id)
//        {
//            if (modelstate.isvalid)
//            {

//                var recordsselected = selectrewards(id);
//                var recordsbloodtype = loadbloodtype().tolist();
//                selectlist bloodtype = new selectlist(recordsbloodtype, "bloodtypeid", "bloodtypecb");
//                viewbag.bloodtype = bloodtype;


//                var model = new rewards()
//                {
//                    rewardid = recordsselected.rewardid,
//                    isactive = recordsselected.isactive,
//                    description = recordsselected.description,
//                    isgovernment = recordsselected.isgovernment,
//                    note = recordsselected.note,
//                    bloodtype = recordsselected.bloodtype,
//                    minrequirement = recordsselected.minrequirement,
//                    physicallocation = recordsselected.physicallocation,
//                    maxclaim = recordsselected.maxclaim
//                };


//                return view(model);
//            }

//            return redirecttoaction("viewrewards");
//        }

//        [httppost]
//        [validateantiforgerytoken]
//        public actionresult rdetails(rewards model)
//        {

//            if (model != null)
//            {
//                int recordsupdated =
//                    updaterewards(model.rewardid,
//                 model.isactive,
//                 model.description, model.note, model.bloodtypeid,
//                 model.minrequirement, model.maxclaim, model.physicallocation);
//                return redirecttoaction("viewrewards");
//            }
//            return view(model);
//        }

//        [validateinput(false)]
//        public actionresult deletereward(string id)
//        {

//            if (modelstate.isvalid)
//            {

//                var recordsdelete = deleterewards(id);

//                return redirecttoaction("viewrewards");
//            }
//            return redirecttoaction("viewrewards");
//        }

//        public actionresult viewclaimrecords()
//        {
//            list<claim_rewards_records> rewards = new list<claim_rewards_records>();
//            var data = loadclaimedrewards();

//            foreach (var row in data)
//            {
//                rewards.add(new claim_rewards_records
//                {
//                    userid = row.userid,
//                    appointmentid = row.appointmentid,
//                    completiontime = row.completiontime,
//                    rewardid = row.rewardid,
//                    claimtime = row.claimtime
//                });
//            }

//            return view(rewards);
//        }

//    }
//}