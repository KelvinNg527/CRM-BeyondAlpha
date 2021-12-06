using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ProjectModel
    {
        public string admin_ID { get; set; }

        [Display(Name = "Project ID")]
        public string project_ID { get; set; }
        [Display(Name = "Project Title")]
        public string project_Title { get; set; }
        [Display(Name = "Project Status")]
        public string project_Status { get; set; }
        [Display(Name = "Project Progress")]
        public double project_Progress { get; set; }
        [Display(Name = "Project Start Date")]
        public DateTime project_StartDate { get; set; }
        [Display(Name = "Project End Date")]
        public DateTime project_EndDate { get; set; }
        [Display(Name = "Project Desc")]
        public string project_Desc { get; set; }

        public List<ProjectModel> TaskList { get; set; }

        //Task
        [Display(Name = "Task ID")]
        public string task_ID { get; set; }
        [Display(Name = "Task Title")]
        public string task_Title { get; set; }
        public string task_Status { get; set; }
        public string task_MemberID { get; set; }
        public string task_ManagerID { get; set; }
    }
}
