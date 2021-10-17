using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health4U_Admin_.Controllers
{
    using System.Drawing;
    using System.Runtime.Serialization.Formatters.Binary;
    using static DataLibrary.BusinessLogic.DiseaseProcessor;
    public class DiseasesController : Controller
    {
        // GET: Diseases
        public ActionResult AddDisease()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDisease(HttpPostedFileBase file, HttpPostedFileBase file2, Diseases model)
        {
            if (model.Description != null)
            {
                List<Diseases> Diseases = new List<Diseases>();
                Diseases app = new Diseases();

                var data = LoadDisease();
                var lastDiseaseID = data.AsQueryable().OrderByDescending(c => c.DiseaseID).FirstOrDefault();
                if (lastDiseaseID == null)
                {
                    app.DiseaseID = "D0001";
                }
                else
                {
                    app.DiseaseID = "D" + (Convert.ToInt32(lastDiseaseID.DiseaseID.Substring
                        (1, lastDiseaseID.DiseaseID.Length - 1)) + 1).ToString("D4");
                }


                MemoryStream ms = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();

                if (file == null && file2 == null)
                {
                    var recordsCreated = CreateDisease(app.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, null, null);
                }
                else if (file2 == null)
                {
                    file.InputStream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array = ms.GetBuffer();
                    var recordsCreated = CreateDisease(app.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, array, null);
                }
                else if (file == null)
                {
                    file2.InputStream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array = ms.GetBuffer();
                    var recordsCreated = CreateDisease(app.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, null, array);
                }
                else
                {
                    file.InputStream.CopyTo(ms);
                    file2.InputStream.CopyTo(ms2);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array = ms.GetBuffer();
                    byte[] array2 = ms2.GetBuffer();

                    var recordsCreated = CreateDisease(app.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, array, array2);
                }
            }
            // after successfully uploading redirect the user
            return RedirectToAction("ViewDisease", "Disease");
        }

        public ActionResult Details(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsSelected = SelectOneDisease(id);
                ViewBag.id = id;

                if (recordsSelected.Image1 == null && recordsSelected.Image2 == null)
                {
                    string imgPath = Server.MapPath("~/Assets/img/NoImage.png");
                    // Convert image to byte array  
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);

                    var model = new Diseases()
                    {
                        DiseaseID = recordsSelected.DiseaseID,
                        Description = recordsSelected.Description,
                        Treatment = recordsSelected.Treatment,
                        Causes = recordsSelected.Causes,
                        Symptoms = recordsSelected.Symptoms,
                        Image1 = byteData,
                        Image2 = byteData
                    };
                    return View(model);
                }
                else if (recordsSelected.Image1 == null)
                {
                    string imgPath = Server.MapPath("~/Assets/img/NoImage.png");
                    // Convert image to byte array  
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);

                    var model = new Diseases()
                    {
                        DiseaseID = recordsSelected.DiseaseID,
                        Description = recordsSelected.Description,
                        Treatment = recordsSelected.Treatment,
                        Causes = recordsSelected.Causes,
                        Symptoms = recordsSelected.Symptoms,
                        Image1 = byteData,
                        Image2 = recordsSelected.Image2
                    };
                    return View(model);
                }
                else if (recordsSelected.Image2 == null)
                {
                    string imgPath = Server.MapPath("~/Assets/img/NoImage.png");
                    // Convert image to byte array  
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);

                    var model = new Diseases()
                    {
                        DiseaseID = recordsSelected.DiseaseID,
                        Description = recordsSelected.Description,
                        Treatment = recordsSelected.Treatment,
                        Causes = recordsSelected.Causes,
                        Symptoms = recordsSelected.Symptoms,
                        Image1 = recordsSelected.Image1,
                        Image2 = byteData
                    };
                    return View(model);
                }
                else
                {
                    var model = new Diseases()
                    {
                        DiseaseID = recordsSelected.DiseaseID,
                        Description = recordsSelected.Description,
                        Treatment = recordsSelected.Treatment,
                        Causes = recordsSelected.Causes,
                        Symptoms = recordsSelected.Symptoms,
                        Image1 = recordsSelected.Image1,
                        Image2 = recordsSelected.Image2
                    };
                    return View(model);
                }
            }
            return RedirectToAction("ViewDisease", "Disease");

        }

        public ActionResult UpdateDisease(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsSelected = SelectOneDisease(id);
                    var model = new Diseases()
                    {
                        DiseaseID = recordsSelected.DiseaseID,
                        Description = recordsSelected.Description,
                        Treatment = recordsSelected.Treatment,
                        Causes = recordsSelected.Causes,
                        Symptoms = recordsSelected.Symptoms,
                    };
                    return View(model);
                    
            }
            return RedirectToAction("ViewDisease", "Disease");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDisease(HttpPostedFileBase file, HttpPostedFileBase file2, Diseases model)
        {

            var recordsSelected = SelectOneDisease(model.DiseaseID);

            MemoryStream ms = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            if (model != null)
            {
                if (file == null)
                {
                    file2.InputStream.CopyTo(ms2);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array2 = ms2.GetBuffer();
                    var recordsCreated = UpdateDiseases(model.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, recordsSelected.Image1, array2);
                }else if(file2==null)
                {
                    file.InputStream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array = ms.GetBuffer();
                    var recordsCreated = UpdateDiseases(model.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, array,recordsSelected.Image2 );
                }else if(file==null&& file2== null)
                {
                    file.InputStream.CopyTo(ms);
                    file2.InputStream.CopyTo(ms2);
                    byte[] array = ms.GetBuffer();
                    byte[] array2 = ms2.GetBuffer();

                    var recordsCreated = UpdateDiseases(model.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, array, array2);
                }
                else
                {
                    file.InputStream.CopyTo(ms);
                    file2.InputStream.CopyTo(ms2);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] array = ms.GetBuffer();
                    byte[] array2 = ms2.GetBuffer();
                    var recordsCreated = UpdateDiseases(model.DiseaseID, model.Description,
                model.Treatment, model.Causes, model.Symptoms, array, array2);
                }
                return RedirectToAction("ViewDisease");
            }
            return View(model);
        }

        public ActionResult ViewDisease()
        {
            List<Diseases> Disease = new List<Diseases>();

            var data = LoadDisease();

            foreach (var row in data)
            {
                Disease.Add(new Diseases
                {
                    DiseaseID = row.DiseaseID,
                    Description = row.Description,
                    Treatment = row.Treatment,
                    Causes = row.Causes,
                    Symptoms = row.Symptoms
                });
            }
            return View(Disease);
        }

        [ValidateInput(false)]
        public ActionResult DeleteDisease(string id)
        {

            if (ModelState.IsValid)
            {

                var recordsDelete = DeleteDiseases(id);

                return RedirectToAction("ViewDisease");
            }
            return RedirectToAction("ViewDisease");
        }


    }
}