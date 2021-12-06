using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace CRM.Controllers
{
    using System.Net;
    using System.Net.Mail;
    using static DataLibrary.BusinessLogic.TaskProcessor;
    using static DataLibrary.BusinessLogic.EmailProcessor;

    public class ProjectController : Controller
    {
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(ProjectModel model)
        {
            List<ProjectModel> Task = new List<ProjectModel>();
            ProjectModel app = new ProjectModel();

            var data = LoadProject();

            var lastProject = data.AsQueryable().OrderByDescending(c => c.project_ID).FirstOrDefault();

            if (lastProject == null)
            {
                app.project_ID = "P001";
            }
            else
            {
                app.project_ID = "P"+(Convert.ToInt32(lastProject.project_ID.Substring
                    (1, lastProject.project_ID.Length - 1)) + 1).ToString("D3");
            }
            int recordsCreated = CreateProject(app.project_ID,
         model.project_Desc, model.project_StartDate, model.project_EndDate);

            return RedirectToAction("index", "Home");

        }

        public ActionResult AddTask()
        {

            var recordsAdmin = LoadAdmin().ToList();


            SelectList adminList = new SelectList(recordsAdmin, "admin_id", "AdminShort");
            ViewBag.adminList = adminList;
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(ProjectModel model)
        {
            List<ProjectModel> Task = new List<ProjectModel>();
            ProjectModel app = new ProjectModel();
            var content = "";
            content = TempData["PassProject"] as string;


            var data = LoadTask();

            var lastTask = data.AsQueryable().OrderByDescending(c => c.task_ID).FirstOrDefault();

            if (lastTask == null)
            {
                app.task_ID = "T001";
            }
            else
            {
                app.task_ID = "T" + (Convert.ToInt32(lastTask.task_ID.Substring
                    (1, lastTask.task_ID.Length - 1)) + 1).ToString("D3");
            }
            int recordsCreated = CreateTask(app.task_ID, content,
         model.task_Title, model.task_Status, model.task_MemberID,model.task_ManagerID);

            if(recordsCreated!=0)
            {
                var adminselected = SelectAdmin(model.task_MemberID);
            try
            {
                    if (ModelState.IsValid)
                    {
                        var senderEmail = new MailAddress("kelvinng5270@gmail.com", "Admin CRM");
                        var receiverEmail = new MailAddress(adminselected.admin_emailAddress, adminselected.admin_name);
                        var password = "52721005272100";
                        var sub = "Task ID " + app.task_ID + " (" + model.task_Title + ") assigned";
                        var body = "Dear Sir/Madam," + Environment.NewLine + Environment.NewLine +
                       "Your task had been assigned with the title of " + model.task_Title + Environment.NewLine+"Please complete your task before deadline. Thanks You!"
                     + Environment.NewLine + Environment.NewLine +
                        "Best Regards" + Environment.NewLine + "Admin CRM";
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                    } 
                }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
                    return RedirectToAction("index", "Home");

                }
            }
            return RedirectToAction("index", "Home");

        }



        [HttpGet]
        public ActionResult Details(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsselected = SelectProject(id);

                var model = new ProjectModel()
                {
                    project_ID = recordsselected.project_ID,
                    project_Status = recordsselected.project_Status,
                    project_Desc = recordsselected.project_Desc,
                    project_StartDate = recordsselected.project_StartDate,
                    project_EndDate = recordsselected.project_EndDate,
                    
                };
                var selectProjectList = SelectTask(recordsselected.project_ID);

                var TaskList = new ProjectModel();
                TaskList.TaskList = new List<ProjectModel>();
                foreach (var rows in selectProjectList)
                {
                    TaskList.TaskList.Add(new ProjectModel()
                    {
                        task_ID = rows.task_ID,
                        task_Title = rows.task_Title,
                        task_Status = rows.task_Status,
                    });
                }
                ViewData["TaskList"] = TaskList;

                var selectAdminList = SelectTaskAdmin(recordsselected.project_ID);

                var AdminList = new Admin();
                AdminList.AdminList = new List<Admin>();
                foreach (var rows in selectAdminList)
                {
                    AdminList.AdminList.Add(new Admin()
                    {
                        admin_id=rows.admin_id,
                        admin_name=rows.admin_name
                    });
                }
                ViewData["AdminList"] = AdminList;


                TempData["PassProject"] = id;

                return View(model);
            }

            return RedirectToAction("AddProject");
        }

        [HttpPost]
        public ActionResult TaskDetail(ProjectModel model)
        {

            if (model != null)
            {
                int recordsupdated =
                    UpdateTask(model.task_ID,model.task_Title, model.task_Status,
                 model.task_MemberID, model.task_ManagerID);
                 
                 return RedirectToAction("index", "home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult TaskDetail(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsselected = SelectOTask(id);
                var recordsAdmin = LoadAdmin().ToList();


                SelectList adminList = new SelectList(recordsAdmin, "admin_id", "AdminShort");
                ViewBag.adminList = adminList;
                var model = new ProjectModel()
                {
                    task_ID = recordsselected.task_ID,
                    task_Title = recordsselected.task_Title,
                    task_Status = recordsselected.task_Status,
                    task_MemberID = recordsselected.task_MemberID,
                    task_ManagerID = recordsselected.task_ManagerID,

                };

                return View(model);
            }

            return RedirectToAction("index", "home");
        }


        [HttpGet]
        public ActionResult EditDetails(string id)
        {
            if (ModelState.IsValid)
            {
                var recordsselected = SelectProject(id);

                var model = new ProjectModel()
                {
                    project_ID = recordsselected.project_ID,
                    project_Status = recordsselected.project_Status,
                    project_Desc = recordsselected.project_Desc,
                    project_StartDate = recordsselected.project_StartDate,
                    project_EndDate = recordsselected.project_EndDate,

                };
               
                return View(model);
            }

            return RedirectToAction("index","home");
        }
        [HttpPost]
        public ActionResult EditDetails(ProjectModel model)
        {

            if (model != null)
            {
                int recordsupdated =
                    UpdateProject(model.project_ID,model.project_Desc,
                 model.project_StartDate, model.project_EndDate,
                 model.project_Title, model.project_Progress,model.project_Status);
                return RedirectToAction("index", "home");
            }
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult DeleteTask(string id)
        {

            if (id != null)
            {
                var recordDelete = DeleteOTask(id);

                return RedirectToAction("index", "home");
            }
            return RedirectToAction("index", "home");

        }
    }
}