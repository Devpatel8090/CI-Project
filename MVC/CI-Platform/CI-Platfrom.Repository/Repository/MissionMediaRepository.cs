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
    public class MissionMediaRepository : Repository<MissionMedium>,IMissionMediaRepository
    {
        private readonly CiPlatformContext _db;

        public MissionMediaRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }
        public List<MissionMedium> GetAllMissionMedia()
        {
            List<MissionMedium> missionMediaDetails = _db.MissionMedia.ToList();
            return missionMediaDetails;
        }
    }
}
