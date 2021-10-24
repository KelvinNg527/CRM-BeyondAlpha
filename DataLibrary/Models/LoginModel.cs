using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class LoginModel
    {
        public LoginModel() { }
        public LoginModel(string ID, string Password, string Name = null, string Position = null)
        {
            this.ID = ID;
            this.Password = Password;
            this.Name = Name;
            this.Position = Position;
        }

        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
