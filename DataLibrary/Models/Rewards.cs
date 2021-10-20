using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataLibrary.Models
{
    public class Rewards
    {

        public string RewardID { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool IsGovernment { get; set; }
        public string Note { get; set; }
        public int BloodTypeID { get; set; }
        public string BloodType { get; set; }
        public int BloodVolume { get; set; }
        public int MinRequirement { get; set; }
        public string PhysicalLocation { get; set; }
        public int MaxClaim { get; set; }

        public string BloodTypeCb
        {
            get { return BloodType + "-" + BloodVolume; }
            set { }
        }
    }
}
