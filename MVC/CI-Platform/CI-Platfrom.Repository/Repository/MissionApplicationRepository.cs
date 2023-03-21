﻿using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
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
        public List<MissionApplication> GetUsersByMissionId(long missionId)
        {
            List<MissionApplication> missionApplication = _db.MissionApplications.Where(e => e.MissionId == missionId).ToList();
            return missionApplication;
        }
    }
}
