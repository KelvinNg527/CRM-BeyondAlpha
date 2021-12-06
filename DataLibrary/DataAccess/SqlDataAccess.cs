using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using DataLibrary.Models;

namespace DataLibrary.DataAccees
{
    class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "DefaultConnection")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            try
            {
                using (var cnn = new MySqlConnection(GetConnectionString()))
                {
                    return cnn.Query<T>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Add Data+Update Date
        public static int SaveData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                //cnn.Open();
                return cnn.Execute(sql, data);
            }
        }

        public static int DeleteData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                var isSuccess = 0;
                // cnn.Open();
                var param = new DynamicParameters();
                if (data is String)
                {
                    param.Add("@task_ID", data);
                    isSuccess = cnn.Execute(sql, param);
                }
                else
                {
                    isSuccess = cnn.Execute(sql, data);
                }
                return isSuccess;
            }
        }

        public static int DeleteEData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                var isSuccess = 0;
                // cnn.Open();
                var param = new DynamicParameters();
                if (data is String)
                {
                    param.Add("@id", data);
                    isSuccess = cnn.Execute(sql, param);
                }
                else
                {
                    isSuccess = cnn.Execute(sql, data);
                }
                return isSuccess;
            }
        }

        public static int DeleteDData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                var isSuccess = 0;
                // cnn.Open();
                var param = new DynamicParameters();
                if (data is String)
                {
                    param.Add("@DiseaseID", data);
                    isSuccess = cnn.Execute(sql, param);
                }
                else
                {
                    isSuccess = cnn.Execute(sql, data);
                }
                return isSuccess;
            }
        }
        public static T SelectProject<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.ProjectModel)
                {
                    var model = data as ProjectModel;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { project_ID = model.project_ID }), typeof(T));
                }

                return default(T);
            }
        }

        public static T SelectAdminID<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.Admin)
                {
                    var model = data as Admin;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { admin_id = model.admin_id }), typeof(T));
                }

                return default(T);
            }
        }
        public static T SelectTask<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.ProjectModel)
                {
                    var model = data as ProjectModel;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { task_ID = model.task_ID }), typeof(T));
                }

                return default(T);
            }
        }
        public static T SelectEmail<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.Email)
                {
                    var model = data as Email;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { email_ID = model.email_ID }), typeof(T));
                }

                return default(T);
            }
        }

        public static T SelectEmailAdd<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.Email)
                {
                    var model = data as Email;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { user_email = model.user_email }), typeof(T));
                }

                return default(T);
            }
        }








    }
}


