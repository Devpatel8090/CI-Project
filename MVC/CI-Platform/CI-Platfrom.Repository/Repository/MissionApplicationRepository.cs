using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class MissionApplicationRepository: Repository<MissionApplication>,IMissionApplicationRepository
    {

        private readonly CiPlatformContext _db;
        public MissionApplicationRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }
        public List<MissionApplication> GetMissionApplications()
        {
            List<MissionApplication> missionApplication = _db.MissionApplications.Include(m=> m.Mission).Include(m => m.User).ToList();
            return missionApplication;
        }
        public List<MissionApplication> GetUsersByMissionId(long missionId)
        {
            List<MissionApplication> missionApplication = _db.MissionApplications.Where(e => e.MissionId == missionId).ToList();
            return missionApplication;
        }

        public List<MissionApplication> getUserMissions(long userId)
        {
            List<MissionApplication> missionDetails = _db.MissionApplications.Include(e=> e.Mission).Include(e => e.User).Where(e => e.UserId == userId).ToList();
            return missionDetails;
        }
    }
}
