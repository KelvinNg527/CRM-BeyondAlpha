using DataLibrary.DataAccees;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class RewardsProcessor
    {
        public static List<Rewards> LoadRewards()
        {

            string sql = @"SELECT r.rewardID,r.IsActive,r.Description,r.IsGovernment,r.Note,r.MinRequirement
             ,r.PhysicalLocation,r.MaxClaim,b.BloodTypeID,b.BloodType,b.BloodVolume FROM rewards r JOIN blood_type b
            ON r.BloodType=b.BloodTypeID ;";
            return SqlDataAccess.LoadData<Rewards>(sql);
        }

        public static List<Rewards> LoadBloodType()
        {

            string sql = @"select* from blood_type";
            return SqlDataAccess.LoadData<Rewards>(sql);
        }

        public static Rewards SelectRewards(string RewardID)
        {

            string sql = @"SELECT r.RewardID,r.IsActive,r.Description,r.IsGovernment,r.Note,
        r.BloodType,r.MinRequirement,r.PhysicalLocation,r.MaxClaim,b.BloodType,b.BloodVolume FROM rewards r 
        JOIN blood_type b ON r.BloodType=b.BloodTypeID where RewardID=@RewardID ;";
            return SqlDataAccess.SelectRData<Rewards>(sql, new Rewards() { RewardID = RewardID });
        }

        public static List<Rewards> LoadIsActive()
        {

            string sql = @"SELECT Distinct(IsActive) FROM rewards;";
            return SqlDataAccess.LoadData<Rewards>(sql);
        }

        public static int UpdateRewards(string RewardID, bool IsActive,
          string Description, string Note, int BloodTypeID,
          int MinRequirement,int MaxClaim)
        {

            Rewards data = new Rewards
            {
                RewardID = RewardID,
                IsActive = IsActive,
                Description = Description,
                Note=Note,
                BloodTypeID = BloodTypeID,
                MinRequirement = MinRequirement,
                MaxClaim= MaxClaim
            };

            string sql = @"Update rewards
                set 
                   IsActive=@IsActive,
                    Description=@Description,
                    Note=@Note
                    BloodType=@BloodTypeID,
                    MinRequirement=@MinRequirement,
                     MaxClaim=@MaxClaim
                    where RewardID=@RewardID";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int DeleteRewards(string RewardID)
        {
            string sql = @"delete from  rewards where RewardID= @RewardID;";
            return SqlDataAccess.DeleteRData(sql, RewardID);
        }
        
    }
}
