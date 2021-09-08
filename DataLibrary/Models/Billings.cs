﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataLibrary.Models
{
    public class Billings
    {
        public string BillID { get; set; }
        public string CorporateID { get; set; }
        public DateTime BillDate { get; set; }
        public int PackageID { get; set; }
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Please enter valid Subscribe Month")]
        public int SubscribeMonth { get; set; }
        public int PricePerMonth { get; set; }
        public string AdminID { get; set; }
        public int Total { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelephoneNo { get; set; }

    }  
}
