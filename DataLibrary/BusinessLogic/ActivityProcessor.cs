using DataLibrary.DataAccees;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class ActivityProcessor
    {
        public static List<ActivityModel> LoadPendingActivity()
        {
            string sql = @"SELECT e.*, s.Description from blood_donation_event e JOIN event_status s ON (e.StatusID = s.StatusID) WHERE e.StatusID = 1;";
            return SqlDataAccess.LoadData<ActivityModel>(sql);
        }

        public static ActivityModel SelectEventInfor(int EventID)
        {
            string query = @"SELECT e.*, s.Description, c.Name from blood_donation_event e INNER JOIN event_status s ON (e.StatusID = s.StatusID) INNER JOIN corporate c ON (e.CorporateID = c.CorporateID);";
            return SqlDataAccess.SelectEventInfor<ActivityModel>(query, new ActivityModel() { EventID = EventID });
        }

        public static int UpdateApNn(int EventID, string ApprovedBy)
        {
            ActivityModel data = new ActivityModel
            {
                EventID = EventID,
                ApprovedBy = ApprovedBy
            };

            string sql = @"UPDATE blood_donation_event
                                SET
                            StatusID=2,
                            ApprovedBy=@ApprovedBy
                            WHERE EventID=@EventID;";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateApYn(int EventID, string Note, string ApprovedBy)
        {
            ActivityModel data = new ActivityModel
            {
                EventID = EventID,
                Note = Note,
                ApprovedBy = ApprovedBy
            };

            string sql = @"UPDATE blood_donation_event
                                SET
                            StatusID=2,
                            Note=@Note,
                            ApprovedBy=@ApprovedBy
                            WHERE EventID=@EventID;";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateApFNn(int EventID, string ApprovedBy)
        {
            ActivityModel data = new ActivityModel
            {
                EventID = EventID,
                ApprovedBy = ApprovedBy
            };

            string sql = @"UPDATE blood_donation_event
                                SET
                            StatusID=2,
                            ApprovedBy=@ApprovedBy
                            WHERE EventID=@EventID;";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateApFYn(int EventID, string Note, string ApprovedBy)
        {
            ActivityModel data = new ActivityModel
            {
                EventID = EventID,
                Note = Note,
                ApprovedBy = ApprovedBy
            };

            string sql = @"UPDATE blood_donation_event
                                SET
                            StatusID=2,
                            Note=@Note,
                            ApprovedBy=@ApprovedBy
                            WHERE EventID=@EventID;";
            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
