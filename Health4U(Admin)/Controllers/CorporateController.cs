using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace Health4U_Admin_.Controllers
{
    using System.IO;
    using static DataLibrary.BusinessLogic.CorporateProcessor;
    public class CorporateController : Controller
    {

        public ActionResult ViewCorporate()
        {
            List<Corporate> Corporate = new List<Corporate>();

            var data = LoadCorporate();

            foreach (var row in data)
            {
                Corporate.Add(new Corporate
                {
                    CorporateID = row.CorporateID,
                    Name = row.Name,
                    TelephoneNo = row.TelephoneNo,
                    FaxNo = row.FaxNo,
                    isActive=row.isActive
                });
            }
            return View(Corporate);
        }

        // GET: Corporate
        public ActionResult AddCorporate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddCorporate(Corporate model, HttpPostedFileBase file)
        {
            List<Corporate> Corporate = new List<Corporate>();
            Corporate app = new Corporate();


            var data = LoadCorporate();
            var lastCorporate = data.AsQueryable().OrderByDescending(c => c.CorporateID).FirstOrDefault();
            if (lastCorporate == null)
            {
                app.CorporateID = "C100000001";
            }
            else
            {
                app.CorporateID = "C" + (Convert.ToInt32(lastCorporate.CorporateID.Substring
                    (1, lastCorporate.CorporateID.Length - 1)) + 1).ToString("D9");
            }


            if (file == null)
            {
                int recordsCreated = CreateCorporate(app.CorporateID,
                           model.Name, model.Address, model.TelephoneNo,
                            model.FaxNo, model.Latitude, model.Longitude, model.isHospital, null, true);
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] array = ms.GetBuffer();

                int recordsCreated = CreateCorporate(app.CorporateID,
               model.Name, model.Address, model.TelephoneNo,
                model.FaxNo, model.Latitude, model.Longitude, model.isHospital, array, true);
            }
            return RedirectToAction("ViewCorporate");
        }

        public ActionResult Details(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsSelected = SelectCorporate(id);

                if (recordsSelected.image == null)
                {
                    string imgPath = Server.MapPath("~/Assets/img/NoImage.png");
                    // Convert image to byte array  
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);

                    var model = new Corporate()
                    {
                        CorporateID = recordsSelected.CorporateID,
                        Name = recordsSelected.Name,
                        Address = recordsSelected.Address,
                        TelephoneNo = recordsSelected.TelephoneNo,
                        FaxNo = recordsSelected.FaxNo,
                        Latitude = recordsSelected.Latitude,
                        Longitude = recordsSelected.Longitude,
                        isHospital = recordsSelected.isHospital,
                        image = byteData,
                        isActive = recordsSelected.isActive
                    };
                    return View(model);
                }
                else
                {

                    var model = new Corporate()
                    {
                        CorporateID = recordsSelected.CorporateID,
                        Name = recordsSelected.Name,
                        Address = recordsSelected.Address,
                        TelephoneNo = recordsSelected.TelephoneNo,
                        FaxNo = recordsSelected.FaxNo,
                        Latitude = recordsSelected.Latitude,
                        Longitude = recordsSelected.Longitude,
                        isHospital = recordsSelected.isHospital,
                        image = recordsSelected.image,
                        isActive = recordsSelected.isActive
                    };
                    return View(model);
                }
            }
            return RedirectToAction("ViewCorporate", "Corporate");

        }

        public ActionResult UpdateCorporate(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsSelected = SelectCorporate(id);
                var model = new Corporate()
                {
                    CorporateID = recordsSelected.CorporateID,
                    Name = recordsSelected.Name,
                    Address = recordsSelected.Address,
                    TelephoneNo = recordsSelected.TelephoneNo,
                    FaxNo = recordsSelected.FaxNo,
                    Latitude = recordsSelected.Latitude,
                    Longitude = recordsSelected.Longitude,
                    isHospital = recordsSelected.isHospital,
                    image = recordsSelected.image,
                    isActive = recordsSelected.isActive
                };
                return View(model);

            }
            return RedirectToAction("ViewDisease", "Disease");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCorporate(HttpPostedFileBase file, Corporate model)
        {

            var recordsSelected = SelectCorporate(model.CorporateID);

          

            MemoryStream ms = new MemoryStream();
            if (model != null)
            {
               
                 if (file != null )
                {
                    file.InputStream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array = ms.GetBuffer();
                    var recordsCreated = UpdateCorporates(model.CorporateID,
                        model.Name,model.TelephoneNo,model.Address,model.Latitude,model.Longitude,model.FaxNo,array,model.isActive);
                }
                else 
                {
               
                    var recordsCreated =  UpdateCorporates(model.CorporateID,
                        model.Name, model.TelephoneNo, model.Address, model.Latitude, model.Longitude, model.FaxNo, recordsSelected.image
                        , model.isActive);
                }
                
                return RedirectToAction("ViewCorporate");
            }
            return RedirectToAction("ViewCorporate");
        }


    }
}