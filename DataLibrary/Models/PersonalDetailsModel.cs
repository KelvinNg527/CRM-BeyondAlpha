using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models
{
    public class PersonalDetailsModel
    {
        public string AdminID { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        //new password
        public string newPassword { get; set; }
        public string confrimPassword { get; set; }
    }
}
