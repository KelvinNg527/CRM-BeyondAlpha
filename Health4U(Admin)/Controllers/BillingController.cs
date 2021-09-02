﻿using System;
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
                var model = new Billings()
                {
                    BillID = recordsSelected.BillID,
                    CorporateID = recordsSelected.CorporateID,
                    BillDate = recordsSelected.BillDate,
                    PackageID = recordsSelected.PackageID,
                    PackageName = recordsSelected.PackageName,
                    SubscribeMonth = recordsSelected.SubscribeMonth,
                    AdminID = recordsSelected.AdminID,
                    Total = recordsSelected.Total
                };


                //return RedirectToAction("Details");
                return View(model);
            }

            return RedirectToAction("ViewBilling", "Error in selecting ID");
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