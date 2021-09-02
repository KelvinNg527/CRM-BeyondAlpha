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
                    param.Add("@Billings", data);
                    isSuccess = cnn.Execute(sql, param);
                }
                else
                {
                    isSuccess = cnn.Execute(sql, data);
                }
                return isSuccess;
            }
        }


        public static T SelectData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.Billings)
                {
                    var model = data as Billings;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { BillID = model.BillID }), typeof(T));
                }

                return default(T); 
            }
        }

        //public static T SelectTData<T>(string sql, T data)
        //{
        //    using (var cnn = new MySqlConnection(GetConnectionString()))
        //    {
        //        // cnn.Open();

        //        if (data is Models.TreatmentModel)
        //        {
        //            var model = data as TreatmentModel;
        //            return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
        //              new { AppointmentID = model.AppointmentID }), typeof(T));
        //        }

        //        return default(T); //Default
        //    }
        //}

    }
}

//var param = new DynamicParameters();
//                if(data is String) { 
//                    param.Add("@AppointmentID", data.ToString());
