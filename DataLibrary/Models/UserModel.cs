using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(string corporateID)
        {
            CorporateID = corporateID;
        }

        //Doctor View
        public string DoctorID { get; set; }
        public string Name { get; set; }
        public string cName { get; set; }
        public char Gender { get; set; }
        public string SpecificationID { get; set; }
        public string Description { get; set; }
        public string CorporateID { get; set; }
        public DateTime JoinDate { get; set; }
        public string DPassword { get; set; }

        //Nurse View
        public string NurseID { get; set; }

    }
}
