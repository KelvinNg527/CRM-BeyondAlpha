using DataLibrary.DataAccees;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class BillingProcessor
    {

        public static List<Billings> LoadBillings()
        {

            string sql = @"SELECT ab.BillID,ab.CorporateID,ab.BillDate,ab.PackageID,p.PackageName,ab.SubscribeMonth,ab.AdminID,p.PricePerMonth
	,(p.PricePerMonth * ab.SubscribeMonth) AS Total from package p join admin_billing ab on p.PackageID=ab.PackageID;";
            return SqlDataAccess.LoadData<Billings>(sql);
        }
        public static Billings SelectBill(string BillID)
        {

            string sql = @"SELECT ab.BillID,ab.CorporateID,ab.BillDate,ab.PackageID,p.PackageName,ab.SubscribeMonth,ab.AdminID,p.PricePerMonth,c.Name,c.Address,c.TelephoneNo
	,(p.PricePerMonth * ab.SubscribeMonth) AS Total from package p join admin_billing ab
	 on p.PackageID=ab.PackageID JOIN corporate c
	 ON c.CorporateID=ab.CorporateID
	where ab.BillID=@BillID;";
            return SqlDataAccess.SelectData<Billings>(sql, new Billings() { BillID = BillID });
        }


  

    }
}
