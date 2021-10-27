using System;
using System.Collections.Generic;
using DataLibrary.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.BillingProcessor;

    public class SubscriptionController : Controller
    {
        // GET: Subscription
        public ActionResult Subscription()
        {
            var user = Session["user"];

            var recordsPackage = LoadPackage().ToList();
            var recordsCorporate = LoadCorporate().ToList();
            SelectList packagelist = new SelectList(recordsPackage, "PackageID", "Package");
            ViewBag.PackageList = packagelist;
            SelectList corporatelist = new SelectList(recordsCorporate, "CorporateID", "Corporate");
            ViewBag.CorporateList = corporatelist;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscription(Billings model)
        {
            if (ModelState.IsValid)
            {
                List<Billings> Bill = new List<Billings>();
                Billings app = new Billings();
                var user = Session["user"] as LoginModel;
                if (user == null) { return RedirectToAction("login", "Home"); }


                var data = LoadBillings();
                var lastBillID = data.AsQueryable().OrderByDescending(c => c.BillID).FirstOrDefault();
                if (lastBillID == null)
                {
                    app.BillID = "A00001";
                }
                else
                {
                    app.BillID = "A" + (Convert.ToInt32(lastBillID.BillID.Substring
                        (1, lastBillID.BillID.Length - 1)) + 1).ToString("D5");
                }

                DateTime BillDate = model.BillDate;
                DateTime modifiedDatetime = BillDate.AddMonths(model.SubscribeMonth);


                int recordsCreated = CreateBill(app.BillID,
                   model.BillDate,
                   model.PackageID, model.CorporateID,
                    model.SubscribeMonth,user.ID);

                int recordsUpdate = UpdateExpired(modifiedDatetime,model.PackageID, model.CorporateID);
                return RedirectToAction("Subscription");
            }
            return View(model);
        }
    }
}