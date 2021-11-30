using DataLibrary.Models;
using DataLibrary.DataAccees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class TaskProcessor
    {
        public static List<ProjectModel> LoadProject()
        {

            string sql = @"SELECT * from project";
            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }

        public static List<ProjectModel> LoadTask()
        {

            string sql = @"SELECT * from task_management";
            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }

        public static List<Admin> LoadAdmin()
        {

            string sql = @"SELECT * from admin";
            return SqlDataAccess.LoadData<Admin>(sql);
        }

        public static List<ProjectModel> SelectTask(string project_ID)
        {

            string sql = $@"SELECT * from task_management where project_ID='{project_ID}';";
            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }

        public static List<Admin> SelectTaskAdmin(string project_ID)
        {

            string sql = $@"SELECT tm.project_ID,tm.task_MemberID,ad.admin_id,ad.admin_name FROM task_management tm JOIN admin ad on tm.task_MemberID=ad.admin_id
            or tm.task_ManagerID=ad.admin_id where  project_ID='{project_ID}';";
            return SqlDataAccess.LoadData<Admin>(sql);
        }


        public static ProjectModel SelectProject(string project_ID)
        {
            string sql = @"SELECT * from project where project_ID=@project_ID;";
            return SqlDataAccess.SelectProject<ProjectModel>(sql, new ProjectModel() { project_ID = project_ID });
        }

        public static ProjectModel SelectOTask(string task_ID)
        {
            string sql = @"SELECT * from task_management where task_ID=@task_ID;";
            return SqlDataAccess.SelectTask<ProjectModel>(sql, new ProjectModel() { task_ID = task_ID });
        }


        public static int CreateProject(string project_ID,string project_Desc,DateTime project_StartDate,DateTime project_EndDate)
        {
            ProjectModel data = new ProjectModel
            {
                project_ID = project_ID,
                project_Desc = project_Desc,
                project_StartDate = project_StartDate,
                project_EndDate = project_EndDate
            };

            string sql = @"insert into project(project_ID,project_Desc,project_StartDate,project_EndDate)
            values(@project_ID,@project_Desc,@project_StartDate,@project_EndDate);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int CreateTask(string task_ID, string project_ID, string task_Title, 
            string task_Status,string task_MemberID,string task_ManagerID)
        {
            ProjectModel data = new ProjectModel
            {
                task_ID = task_ID,
                project_ID = project_ID,
                task_Title = task_Title,
                task_Status = task_Status,
                task_MemberID= task_MemberID,
                task_ManagerID= task_ManagerID
            };

            string sql = @"insert into task_management(task_ID,project_ID,task_Title,task_Status,task_MemberID,task_ManagerID)
            values(@task_ID,@project_ID,@task_Title,@task_Status,task_MemberID,task_ManagerID);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateProject(string project_ID,string project_Desc, DateTime project_StartDate,
     DateTime project_EndDate, string project_Title, double project_Progress,string project_status)
        {

            ProjectModel data = new ProjectModel
            {
                project_ID = project_ID,
                project_Desc = project_Desc,
                project_StartDate = project_StartDate,
                project_EndDate = project_EndDate,
                project_Title = project_Title,
                project_Progress = project_Progress,
                project_Status=project_status
            };

            string sql = @"Update project
                set 
                   project_ID=@project_ID,
                    project_Title=@project_Title,
                    project_Desc=@project_Desc,
                    project_Status=@project_Status,
                    project_Progress=@project_Progress,
                     project_StartDate=@project_StartDate,
                    project_EndDate=@project_EndDate
                    where project_ID=@project_ID";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int DeleteOTask(string id)
        {

            string sql = @"DELETE FROM task_management WHERE task_ID=@task_ID;";
            return SqlDataAccess.DeleteData(sql, id);
        }

        public static int UpdateTask(string task_ID, string task_Title, string task_Status,
  string task_MemberID, string task_ManagerID)
        {

            ProjectModel data = new ProjectModel
            {
                task_ID = task_ID,
                task_Title = task_Title,
                task_Status = task_Status,
                task_MemberID = task_MemberID,
                task_ManagerID = task_ManagerID,
    
            };

            string sql = @"Update task_management
                set 
                    task_ID=@task_ID,
                    task_Title=@task_Title,
                    task_Status=@task_Status,
                    task_MemberID=@task_MemberID,
                     task_ManagerID=@task_ManagerID
                    where task_ID=@task_ID";

            return SqlDataAccess.SaveData(sql, data);


        }
    }
}
