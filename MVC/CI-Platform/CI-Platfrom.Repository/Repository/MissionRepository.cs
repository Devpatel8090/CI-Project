using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class MissionRepository : IMissionRepository
    {
        private readonly CiPlatformContext _db;

        public MissionRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.ToList();
            return missionDetails;
        }
    }
}
