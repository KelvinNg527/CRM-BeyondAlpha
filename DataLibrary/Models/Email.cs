using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Email
    {
        [Display(Name = "Email ID")]
        public string email_ID { get; set; }
        [Display(Name = "Email Type")]
        public string email_Name { get; set; }
        [Display(Name = "Email Subject")]
        public string email_subject { get; set; }
        [Display(Name = "Attack Link")]
        public string email_PreviewSubject { get; set; }
        [Display(Name = "Email From")]
        public string email_AddressFrom { get; set; }
        [Display(Name = "Email To")]
        public string email_AddressTo { get; set; }
        [Display(Name = "Content")]
        public string email_Text { get; set; }
        [Display(Name = "User ID")]
        public string user_ID { get; set; }
        [Display(Name = "User Name")]
        public string user_nickname { get; set; }
        [Display(Name = "User Email")]
        public string user_email { get; set; }
        [Display(Name = "User Password")]
        public string user_password { get; set; }


        [Display(Name = "Admin ID")]
        public string admin_ID { get; set; }
        public bool isSend { get; set; }
        public List<Email> UserList { get; set; }
        public string UserShort
        {
            get { return user_ID + "(" + user_nickname + ")"+"-"+user_email; }
            set { }
        }
    }
}
