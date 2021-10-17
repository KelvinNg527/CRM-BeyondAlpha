using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataLibrary.DataAccees;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class DiseaseProcessor
    {

        public static int CreateDisease(string DiseaseID, string Description,
           string Treatment, string Causes, string Symptoms,
             dynamic Image1, dynamic Image2)
        {

            var args = new DynamicParameters();
            args.Add("@DiseaseID", DiseaseID, DbType.String);
            args.Add("@Description", Description, DbType.String);
            args.Add("@Treatment", Treatment, DbType.String);
            args.Add("@Causes", Causes, DbType.String);
            args.Add("@Symptoms", Symptoms, DbType.String);
            args.Add("@Image1", Image1, DbType.Binary);
            args.Add("@Image2", Image2, DbType.Binary);


            string sql = @"insert into disease(DiseaseID,Description,Treatment,Causes,
           Symptoms,Image1,Image2)
            values(@DiseaseID,@Description,@Treatment,@Causes,@Symptoms,@Image1,@Image2);";

            return SqlDataAccess.SaveData(sql, args);
        }

        public static List<Diseases> LoadDisease()
        {
            string sql = @"SELECT * from disease;";
            return SqlDataAccess.LoadData<Diseases>(sql);
        }

        public static Diseases SelectOneDisease(string DiseaseID)
        {
            string query = @"SELECT * FROM disease where DiseaseID=@DiseaseID;";
            return SqlDataAccess.SelectdData<Diseases>(query, new Diseases() { DiseaseID = DiseaseID });
        }

        public static int UpdateDiseases(string DiseaseID,string Description,
            string Treatment, string Causes, string Symptoms,
              dynamic Image1, dynamic Image2)
        {

            var args = new DynamicParameters();
            args.Add("@DiseaseID", DiseaseID, DbType.String);
            args.Add("@Description", Description, DbType.String);
            args.Add("@Treatment", Treatment, DbType.String);
            args.Add("@Causes", Causes, DbType.String);
            args.Add("@Symptoms", Symptoms, DbType.String);
            args.Add("@Image1", Image1, DbType.Binary);
            args.Add("@Image2", Image2, DbType.Binary);

            string sql = @"Update disease
                set 
                   Description=@Description,
                    Treatment=@Treatment,
                    Causes=@Causes,
                    Symptoms=@Symptoms,
                    Image1=@Image1,
                    Image2=@Image2
                    where DiseaseID=@DiseaseID";

            return SqlDataAccess.SaveData(sql, args);
        }

        public static int DeleteDiseases(string DiseaseID)
        {
            string sql = @"delete from  disease where DiseaseID= @DiseaseID;";
            return SqlDataAccess.DeleteDData(sql, DiseaseID);
        }
    }
}
