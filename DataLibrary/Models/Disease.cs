using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Diseases
    {

        [Display(Name = "DiseaseID")]
        public string DiseaseID { get; set; }

        [Display(Name = "Disease Name")]
        public string Description { get; set; }

        [Display(Name = "Treatment")]
        public string Treatment { get; set; }

        [Display(Name = "Causes")]
        public string Causes { get; set; }

        [Display(Name = "Symptoms")]
        public string Symptoms { get; set; }

        [Display(Name = "Image1")]
        public byte[] Image1 { get; set; }

        [Display(Name = "Image2")]
        public byte[] Image2 { get; set; }

        [Display(Name = "SpecificationID")]
        public string SpecificationID { get; set; }

        public string DpDisease
        {
            get { return DiseaseID + "-" + Description; }
            set { }
        }
        

    }
}
