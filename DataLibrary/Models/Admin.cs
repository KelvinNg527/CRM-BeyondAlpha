using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Admin
    {
        public string admin_id { get; set; }
        public string admin_name { get; set; }
        public string admin_tel { get; set; }
        public string admin_address { get; set; }
        public string admin_emailAddress { get; set; }
        public List<Admin> AdminList { get; set; }
        public string project_ID { get; set; }
 

        public string AdminShort
        {
            get { return admin_id + "(" + admin_name + ")"; }
            set { }
        }
    }
}
