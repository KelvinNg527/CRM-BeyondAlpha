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

        public static List<Billings> LoadPackage()
        {

            string sql = @"SELECT PackageID from package";
            return SqlDataAccess.LoadData<Billings>(sql);
        }

        public static List<Billings> LoadAdmin()
        {

            string sql = @"SELECT AdminID from admin";
            return SqlDataAccess.LoadData<Billings>(sql);
        }

        public static List<Billings> LoadCorporate()
        {

            string sql = @"SELECT CorporateID from corporate";
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

        public static int UpdateBill(string BillID, string CorporateID,
          string AdminID, DateTime BillDate, int PackageID,
          int SubscribeMonth)
        {

            Billings data = new Billings
            {
                BillID = BillID,
                CorporateID = CorporateID,
                AdminID = AdminID,
                BillDate = BillDate,
                PackageID = PackageID,
                SubscribeMonth = SubscribeMonth,
            };

            string sql = @"Update admin_billing
                set 
                   CorporateID=@CorporateID,
                    AdminID=@AdminID,
                    BillDate=@BillDate,
                    PackageID=@PackageID,
                    SubscribeMonth=@SubscribeMonth
                    where BillID=@BillID";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteBilling(string BillID)
        {
            string sql = @"delete from  admin_billing where BillID= @BillID;";
            return SqlDataAccess.DeleteData(sql, BillID);
        }


    }
}
