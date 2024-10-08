﻿using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class MissionRepository : Repository<Mission>,IMissionRepository
    {
        private new readonly CiPlatformContext _db;

        public MissionRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// For Getting the missions for the landing page
        /// </summary>
        /// <returns></returns>
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _db.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.Timesheets).Include(m => m.MissionMedia).Include(m=> m.MissionRatings).Include(m => m.MissionSkills).Include(m => m.FavoriteMissions).Include(m => m.GoalMissions).Where(m => m.DeletedAt == null).ToList();
            return missionDetails;
        }

        /// <summary>
        /// filter by countryid of all missions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Mission> GetMissionByCountry(long id)
        {
            List<Mission> missionDetailsByCountry = _db.Missions.Where(m => m.CountryId == id).ToList();
            return missionDetailsByCountry;
        }

        /// <summary>
        /// For Getting the mission Detail for Mission Volunteering page
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public Mission GetMissionByMissionId(long missionId)
        {
            var particularMission = _db.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).Include(m => m.MissionSkills).Include(m => m.FavoriteMissions).Include(m => m.MissionRatings).Include(m => m.Comments).Include(m=> m.MissionDocuments).Include( m => m.GoalMissions).FirstOrDefault(m => m.MissionId == missionId && m.DeletedAt == null);
            return particularMission?? new();
        }


        /// <summary>
        /// take the related missions checking criteria is city followed by country and followed by theme and take 3
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public IEnumerable<Mission> getRelatedMissions(long missionId)
        {
            var relatedMissionList = Enumerable.Empty<Mission>();
            var relatedmission = GetMissionDetails().FirstOrDefault( e => e.MissionId == missionId);
            if(relatedmission != null) { 
            long countryId = relatedmission.CountryId;
            long cityId = relatedmission.CityId;
            long themeId = relatedmission.ThemeId;

            //var relatedMissionList = _db.Missions.Where(e => e.CityId == cityId).ToList();
             relatedMissionList = GetMissionDetails().Where(e => e.CityId == cityId && e.MissionId != missionId).ToList();
            if(relatedMissionList.Distinct().Count() < 3)
            {
                //relatedMissionList += _db.Missions.Where(e => e.CountryId == countryId).ToList();   
                var relatedMissionListByCountry = GetMissionDetails().Where(e => e.CountryId == countryId && e.MissionId != missionId).ToList();
                relatedMissionList =  relatedMissionList.Concat(relatedMissionListByCountry).Distinct().ToList();
                if(relatedMissionList.Distinct().Count() < 3)
                {
                    var relatedMissionListByTheme = GetMissionDetails().Where( e => e.ThemeId == themeId && e.MissionId != missionId).ToList(); 
                    relatedMissionList =  relatedMissionList.Concat(relatedMissionListByTheme).Distinct().ToList();
                }

            }

            //relatedMissionList = relatedMissionList.Distinct().ToList();
            relatedMissionList = relatedMissionList.Take(3).ToList();

             
            }
            return relatedMissionList;

        }
    }
}
