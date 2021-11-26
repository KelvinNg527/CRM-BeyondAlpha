using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccees;
using System.Globalization;

namespace DataLibrary.BusinessLogic
{
    public class PersonalDetailsProcessor
    {
        public static PersonalDetailsModel SelectUserA(string AdminID)
        {
            string query = @"SELECT * from admin where AdminID=@AdminID;";
            return SqlDataAccess.SelectUserA<PersonalDetailsModel>(query, new PersonalDetailsModel() { AdminID = AdminID });
        }

        public static int UpdatePasswordA(string AdminID, string newPassword)
        {
            PersonalDetailsModel data = new PersonalDetailsModel
            {
                AdminID = AdminID,
                newPassword = newPassword
            };

            string sql = @"UPDATE admin
                                SET
                            Password=@newPassword
                            WHERE AdminID=@AdminID;";
            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
