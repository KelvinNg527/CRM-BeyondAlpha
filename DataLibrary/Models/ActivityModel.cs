using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ActivityModel
    {
        public int EventID { get; set; }
        public string CorporateID { get; set; }
        public string Name { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string Venue { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Country { get; set; }
        public string ApprovedBy { get; set; }
    }
}
