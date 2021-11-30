using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace CRM.Controllers
{
    using static DataLibrary.BusinessLogic.TaskProcessor;
    public class HomeController : Controller
    {
        // GET: Rewards
        public ActionResult Index()
        {
            List<ProjectModel> Task = new List<ProjectModel>();
            var data = LoadProject();

            foreach (var row in data)
            {
                Task.Add(new ProjectModel
                {
                    project_ID = row.project_ID,
                   project_Title=row.project_Title,
                   project_Progress=row.project_Progress,
                   project_Status=row.project_Status
                });
            }

            return View(Task);
        }

    }
}