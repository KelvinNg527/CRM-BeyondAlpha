using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Corporate
    {
        [Display(Name = "CorporateID")]
        public string CorporateID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "TelephoneNo")]
        public string TelephoneNo { get; set; }

        [Display(Name = "FaxNo")]
        public string FaxNo { get; set; }

        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "isHospital")]
        public bool isHospital { get; set; }

        [Display(Name = "Image")]
        public byte[] image { get; set; }

        [Display(Name = "isActive")]
        public bool isActive { get; set; }

    }
}
