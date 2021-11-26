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
                    param.Add("@BillID", data);
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

        public static int DeleteRData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                var isSuccess = 0;
                // cnn.Open();
                var param = new DynamicParameters();
                if (data is String)
                {
                    param.Add("@RewardID", data);
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

        public static T SelectRData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.Rewards)
                {
                    var model = data as Rewards;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { RewardID = model.RewardID }), typeof(T));
                }

                return default(T);
            }
        }

        public static T SelectCData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {

                if (data is Models.Corporate)
                {
                    var model = data as Corporate;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                        new { CorporateID = model.CorporateID }), typeof(T));
                }

                return default(T);
            }
        }

        public static T SelectdData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                // cnn.Open();

                if (data is Models.Diseases)
                {
                    var model = data as Diseases;
                    return (T)Convert.ChangeType(cnn.QueryFirst<T>(sql,
                      new { DiseaseID = model.DiseaseID }), typeof(T));
                }

                return default(T); //Default
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

        public static T SelectAData<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                var model = data as Models.LoginModel;

                if (data is Models.LoginModel)
                {
                    if (model.ID != null)
                    {
                        return (T)Convert.ChangeType(cnn.QueryFirstOrDefault<T>(sql,
                        new { ID = model.ID, Password = model.Password }), typeof(T));
                    }
                    
                }

                return default(T); //Default
            }
        }

        //Activiy Details SelectEventInfor
        public static T SelectEventInfor<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                if (data is Models.ActivityModel)
                {
                    var model = data as Models.ActivityModel;
                    return (T)Convert.ChangeType(cnn.QueryFirstOrDefault<T>(sql,
                       new { EventID = model.EventID }), typeof(T));
                }

                return default(T); //Default
                //}

                //return new AppointmentModel();
            }
        }

        public static T SelectCor<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                if (data is Models.UserModel)
                {
                    var model = data as Models.UserModel;
                    return (T)Convert.ChangeType(cnn.QueryFirstOrDefault<T>(sql,
                       new { CorporateID = model.CorporateID }), typeof(T));
                }

                return default(T); //Default
                //}

                //return new AppointmentModel();
            }
        }

        public static T SelectUserA<T>(string sql, T data)
        {
            using (var cnn = new MySqlConnection(GetConnectionString()))
            {
                var model = data as Models.PersonalDetailsModel;

                if (data is Models.PersonalDetailsModel)
                {
                    if (model.AdminID != null)
                    {
                        return (T)Convert.ChangeType(cnn.QueryFirstOrDefault<T>(sql,
                        new { AdminID = model.AdminID}), typeof(T));
                    }

                }

                return default(T); //Default
            }
        }
    }
}

//var param = new DynamicParameters();
//                if(data is String) { 
//                    param.Add("@AppointmentID", data.ToString());
