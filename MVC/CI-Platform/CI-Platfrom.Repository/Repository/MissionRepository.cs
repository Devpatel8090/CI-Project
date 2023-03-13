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
    public class MissionRepository : IMissionRepository
    {
        private readonly CiPlatformContext _db;

        public MissionRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).ToList();
            return missionDetails;
        }

        public List<Mission> GetMissionByCountry(long id)
        {
            List<Mission> missionDetailsByCountry = _db.Missions.Where(m => m.CountryId == id).ToList();
            return missionDetailsByCountry;
        }

        //public List<Mission> GetBySort(string sort)
        //{
        //    List<Mission> missionsortdate = GetMissionDetails();

        //    if(sort == "Oldest")
        //    {
        //        return missionsortdate.OrderBy(m => m.CreateAt).ToList();
        //    }
        //    else if(sort == "Newest")
        //    {
        //        return missionsortdate.OrderByDescending(m => m.EndDate).ToList();
        //    }
        //    else if(sort == "Mission Type")
        //    {
        //        return missionsortdate.OrderBy(m => m.MissionType).ToList();
        //    }
        //    else
        //    {
        //        return missionsortdate.OrderBy(m => m.StartDate).ToList();
        //    }
        //}


        //public List<Mission> GetMissionBySort(string sort)
        //{
        //    List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(m => m.Theme).Where(e => e.);
        //}
    }
}
