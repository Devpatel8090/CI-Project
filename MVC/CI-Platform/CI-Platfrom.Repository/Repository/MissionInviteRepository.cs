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
    public class MissionInviteRepository : Repository<MissionInvite>,IMissionInviteRepository
    {
        private new readonly CiPlatformContext _db;
        public MissionInviteRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }
    }
}
