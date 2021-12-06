using DataLibrary.Models;
using DataLibrary.DataAccees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class EmailProcessor
    {
        public static List<Email> LoadEmail()
        {

            string sql = @"SELECT * from automated_email;";
            return SqlDataAccess.LoadData<Email>(sql);
        }

        public static List<Email> LoadUser()
        {
            string sql = @"SELECT * from user";
            return SqlDataAccess.LoadData<Email>(sql);
        }
        public static Email SelectsEmail(string email_ID)
        {
            string sql = @"SELECT * from automated_email where email_ID=@email_ID";
            return SqlDataAccess.SelectEmail<Email>(sql, new Email() { email_ID = email_ID });
        }

        public static Email SelectEmail(string user_email)
        {
            string sql = @"SELECT * FROM user WHERE user_email=@user_email;";
            return SqlDataAccess.SelectEmailAdd<Email>(sql, new Email() { user_email =  user_email});
        }

        public static List<Email> LoadEmailTrue()
        {
            string sql = @"SELECT * from automated_email where isSend=1;";
            return SqlDataAccess.LoadData<Email>(sql);
        }

        public static List<Email> LoadEmailFalse()
        {

            string sql = @"SELECT * from automated_email where isSend=0;";
            return SqlDataAccess.LoadData<Email>(sql);
        }

        public static int CreateEmail(string email_ID, string email_Name, string email_Subject, string email_AddressFrom
            ,string email_AddressTo,string email_Text,string email_PreviewSubject,bool isSend)
        {
            Email data = new Email
            {
                email_ID=email_ID,
                email_Name=email_Name,
                email_subject=email_Subject,
                email_AddressFrom=email_AddressFrom,
                email_AddressTo=email_AddressTo,
                email_Text=email_Text,
                email_PreviewSubject= email_PreviewSubject,
                isSend =isSend
            };

            string sql = @"insert into automated_email(email_ID,email_Name,email_subject,email_AddressFrom,email_AddressTo,email_Text,email_PreviewSubject,isSend)
            values(@email_ID,@email_Name,@email_subject,@email_AddressFrom,@email_AddressTo,@email_Text,@email_PreviewSubject,@isSend);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateEmail(string email_ID, string email_Name, string email_subject,
    string email_AddressFrom, string email_AddressTo, string email_Text,string email_PreviewSubject, bool isSend)
        {

            Email data = new Email
            {
                email_ID = email_ID,
                email_Name = email_Name,
                email_subject = email_subject,
                email_AddressFrom = email_AddressFrom,
                email_AddressTo = email_AddressTo,
                email_Text = email_Text,
                email_PreviewSubject= email_PreviewSubject,
                isSend = isSend
            };

            string sql = @"Update automated_email
                set 
                   email_ID=@email_ID,
                    email_Name=@email_Name,
                    email_subject=@email_subject,
                    email_AddressFrom=@email_AddressFrom,
                    email_AddressTo=@email_AddressTo,
                     email_Text=@email_Text,
                    email_PreviewSubject=@email_PreviewSubject,
                    isSend=@isSend
                    where email_ID=@email_ID";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateStatus( bool isSend,string email_ID)
        {

            Email data = new Email
            {
                email_ID= email_ID,
                isSend = isSend
            };

            string sql = @"Update automated_email
                set 
                    isSend=@isSend
                    where email_ID=@email_ID";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteEmail(string id)
        {

            string sql = @"DELETE FROM automated_email WHERE email_id=@id;";
            return SqlDataAccess.DeleteEData(sql, id);
        }


    }
}
