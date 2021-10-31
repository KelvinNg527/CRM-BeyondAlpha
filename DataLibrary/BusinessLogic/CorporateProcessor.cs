using Dapper;
using DataLibrary.DataAccees;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class CorporateProcessor  
    {

        public static List<Corporate> LoadCorporate()
        {
            string sql = @"SELECT * from corporate";
            return SqlDataAccess.LoadData<Corporate>(sql);
        }
        
         public static Corporate SelectCorporate(string CorporateID)
        {
            string sql = @"SELECT * from corporate where CorporateID=@CorporateID;";
            return SqlDataAccess.SelectCData<Corporate>(sql, new Corporate() { CorporateID = CorporateID });
        }

        public static int CreateCorporate(string CorporateID, string Name, string Address
        ,string TelephoneNo, string FaxNo, string Latitude, string Longitude, bool IsHospital,
           dynamic image, bool IsActive)
        {
            var args = new DynamicParameters();
            args.Add("@CorporateID", CorporateID, DbType.String);
            args.Add("@Name", Name, DbType.String);
            args.Add("@Address", Address, DbType.String);
            args.Add("@TelephoneNo", TelephoneNo, DbType.String);
            args.Add("@FaxNo", FaxNo, DbType.String);
            args.Add("@Latitude", Latitude, DbType.String);
            args.Add("@Longitude", Longitude, DbType.String);
            args.Add("@IsHospital", IsHospital, DbType.Boolean);
            args.Add("@image", image, DbType.Binary);
            args.Add("@IsActive", IsActive, DbType.Boolean);


            string sql = @"insert into corporate(CorporateID,Name,Address,TelephoneNo,
                FaxNo,Latitude,Longitude,isHospital,image,IsActive)
            values(@CorporateID,@Name,@Address,@TelephoneNo,@FaxNo,@Latitude,@Longitude,@isHospital,@image,@IsActive);";
            return SqlDataAccess.SaveData(sql, args);
        }

        public static int UpdateCorporates(string CorporateID, string Name, string TelephoneNo, string Address,
            string Latitude, string Longitude,
            string FaxNo, dynamic image, bool IsActive)
        {

            var args = new DynamicParameters();
            args.Add("@CorporateID", CorporateID, DbType.String);
            args.Add("@Name", Name, DbType.String);
            args.Add("@TelephoneNo", TelephoneNo, DbType.String);
            args.Add("@Address", Address, DbType.String);
            args.Add("@Latitude", Latitude, DbType.String);
            args.Add("@Longitude", Longitude, DbType.String);
            args.Add("@FaxNo", FaxNo, DbType.String);
            args.Add("@image", image, DbType.Binary);
            args.Add("@IsActive", IsActive, DbType.Boolean);

            string sql = @"Update corporate
                set 
                   Name=@Name,
                    TelephoneNo=@TelephoneNo,
                    Address=@Address,
                    Latitude=@Latitude,
                    Longitude=@Longitude,
                    FaxNo=@FaxNo,
                    image=@image,
                    IsActive=@IsActive
                    where CorporateID=@CorporateID";

            return SqlDataAccess.SaveData(sql, args);
        }
    }
}
