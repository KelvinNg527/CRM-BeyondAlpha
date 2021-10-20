using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataLibrary.Models
{
    public class claim_rewards_records
    {
        public int UserID { get; set; }
        public string AppointmentID { get; set; }
        public DateTime CompletionTime { get; set; }
        public string RewardID { get; set; }
        public DateTime ClaimTime { get; set; }
    }
}
