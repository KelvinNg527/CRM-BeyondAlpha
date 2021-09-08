using System;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    using static DataLibrary.BusinessLogic.BillingProcessor;

    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult Viewbilling()
        {
            List<Billings> Billings = new List<Billings>();
            var data = LoadBillings();

            foreach (var row in data)
            {

                Billings.Add(new Billings
                {

                    BillID = row.BillID,
                    CorporateID = row.CorporateID,
                    BillDate = row.BillDate,
                    PackageID = row.PackageID,
                    PackageName=row.PackageName,
                    SubscribeMonth = row.SubscribeMonth,
                    AdminID = row.AdminID,
                    Total=row.Total
                });
            }

            return View(Billings);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if (ModelState.IsValid)
            {

                var recordsSelected = SelectBill(id);
                var recordsPackage= LoadPackage().ToList();
                var recordsAdmin = LoadAdmin().ToList();
                var recordsCorporate = LoadCorporate().ToList();


                SelectList packagelist =new SelectList(recordsPackage, "PackageID", "PackageID");
                ViewBag.PackageList = packagelist;

                SelectList adminlist = new SelectList(recordsAdmin, "AdminID", "AdminID");
                ViewBag.AdminList = adminlist;

                SelectList corporatelist = new SelectList(recordsCorporate, "CorporateID", "CorporateID");
                ViewBag.CorporateList = corporatelist;

                var model = new Billings()
                {
                    BillID = recordsSelected.BillID,
                    CorporateID = recordsSelected.CorporateID,
                    Name = recordsSelected.Name,
                    BillDate = recordsSelected.BillDate,
                    PackageID = recordsSelected.PackageID,
                    PackageName = recordsSelected.PackageName,
                    SubscribeMonth = recordsSelected.SubscribeMonth,
                    AdminID = recordsSelected.AdminID,
                    Total = recordsSelected.Total
            };

            
                return View(model);
            }

            return RedirectToAction("ViewBilling", "Error in selecting ID");
        }

     

        [ValidateInput(false)]
        public ActionResult DeleteBill(string id)
        {

            if (ModelState.IsValid)
            {

                var recordsDelete = DeleteBilling(id);

                return RedirectToAction("ViewBilling");
            }

            return RedirectToAction("ViewBilling", "Error in deletion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Billings model)
        {

            if (model != null)
            {
                int recordsUpdated =
                    UpdateBill(model.BillID,
                 model.CorporateID,
                 model.AdminID, model.BillDate,model.PackageID,model.SubscribeMonth);
                return RedirectToAction("ViewBilling");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Invoices(string id)
        {
            if (ModelState.IsValid)
            {

                var recordsSelected = SelectBill(id);
                var model = new Billings()
                {
                    BillID = recordsSelected.BillID,
                    CorporateID = recordsSelected.CorporateID,
                    BillDate = recordsSelected.BillDate,
                    PackageID = recordsSelected.PackageID,
                    PackageName = recordsSelected.PackageName,
                    SubscribeMonth = recordsSelected.SubscribeMonth,
                    AdminID = recordsSelected.AdminID,
                    PricePerMonth=recordsSelected.PricePerMonth,
                    Total = recordsSelected.Total,
                    Address=recordsSelected.Address,
                    Name=recordsSelected.Name,
                    TelephoneNo=recordsSelected.TelephoneNo
                };
                return View(model);
            }
            return View();
        }
    }
}