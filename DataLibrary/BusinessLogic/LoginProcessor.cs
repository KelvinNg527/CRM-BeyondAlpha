using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccees;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class LoginProcessor
    {
        //SelectAData
        public static LoginModel AdminLogin(string ID, string Password)
        {
            string sql = @"SELECT AdminID as ID, Name, Position, Password FROM admin
                            WHERE AdminID = @ID AND Password = @Password AND IsActive = 1;";

            return SqlDataAccess.SelectAData<LoginModel>(sql, new LoginModel(ID, Password));
        }
    }
}
