using DataLibrary.DataAccees;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class UserProcessor
    {
        public static UserModel CheckCorpo(string CorporateID)
        {
            string sql = @"SELECT * FROM corporate WHERE CorporateID = @CorporateID;";
            return SqlDataAccess.SelectCor<UserModel>(sql, new UserModel(CorporateID));
        }

        public static List<UserModel> LoadFullDoctor()
        {
            string sql = $@"SELECT * from Doctor;";
            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static List<UserModel> LoadFullDoctorCor()
        {
            string sql = $@"SELECT d.*, c.Name as cName from doctor d INNER JOIN corporate c ON d.CorporateID = c.CorporateID;";
            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static int CreateNewUser(string DoctorID, string Name, char Gender, string SpecificationID, string CorporateID)
        {
            DateTime JoinDate = DateTime.Now;

            UserModel data = new UserModel
            {
                DoctorID = DoctorID,
                Name = Name,
                Gender = Gender,
                SpecificationID = SpecificationID,
                CorporateID = CorporateID,
                JoinDate = JoinDate
            };

            string sql = @"INSERT into doctor(DoctorID, Name, CorporateID, Gender, SpecificationID, JoinDate, IsActive, DPassword)
                            values (@DoctorID, @Name, @CorporateID, @Gender, @SpecificationID, @JoinDate, 1, 'abc12345!');";
            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
