using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataLibrary.Models
{
    public class Blacklist
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string reason { get; set; }
        public string staffID { get; set; }
        public string userID { get; set; }
        public DateTime blacklistTime { get; set; }

    }
}
