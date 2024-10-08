﻿using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class MissionVMRepository : IMissionVMRepository
    {

        //private readonly ICityRepository _cities;
        //private readonly ICountryRepository _country;
        //private readonly ISkillRepository _skill;
        //private readonly IThemeRepository _theme;
        //private readonly IUserRepository _user;
        //private readonly IMissionRepository _mission;

        private readonly IUnitOfWorkRepository _unitOfWork;
        

        

        public MissionVMRepository(/*ICityRepository cities, ICountryRepository country, ISkillRepository skill, IThemeRepository theme, IUserRepository user, IMissionRepository mission*/IUnitOfWorkRepository unitOfWork)
        {
            //_cities = cities;
            //_country = country;
            //_skill = skill;
            //_theme = theme;
            //_user = user;
            //_mission = mission;
            _unitOfWork = unitOfWork;


        }

        /// <summary>
        /// By default providing all mission and if country is selected then show the only cities and missions of that country
        /// </summary>
        /// <param name="emailFromSession"></param>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public MissionVM GetAllMissions(string emailFromSession,long CountryId)
        {

            MissionVM missionVM = new();

            IEnumerable<City> cityDetails = _unitOfWork.City.GetCityDetails();
            missionVM.City = cityDetails;

            IEnumerable<Country> countryDetails = _unitOfWork.Country.GetCountriesDetails();
            missionVM.Country = countryDetails;

            IEnumerable<Skill> skillDetails = _unitOfWork.Skill.GetSkillDetails().Where(skill => skill.Status == true);
            missionVM.skills = skillDetails;

            IEnumerable<MissionTheme> themeDetails = _unitOfWork.Theme.GetThemeDetails().Where(theme => theme.Status == true);
            missionVM.MissionTheme = themeDetails;

            IEnumerable<MissionMedium> missionMediumDetails = _unitOfWork.MissionMedia.GetAllMissionMedia();
            missionVM.MissionMedium = missionMediumDetails;

            IEnumerable<Timesheet> timesheets = _unitOfWork.TimeSheet.GetTimesheetDetails().Where(timesheet => timesheet.DeletedAt == null);
            missionVM.timesheets = timesheets;

            

            IEnumerable<User> userDetails = _unitOfWork.User.GetUserDetails().Where(user => user.Status == 1);
            missionVM.User = userDetails;

            IEnumerable<Mission> missionDetails = _unitOfWork.Mission.GetMissionDetails().Where(mission => mission.Status == 1 && mission.Theme.Status == true && mission.Theme.DeletedAt == null);/*GetMissionByCountry(CountryId)*/;
            missionVM.Mission = missionDetails;

            IEnumerable<MissionRating> missionRatings = _unitOfWork.MissionRating.GetAll();
            missionVM.missionRatings = missionRatings;

            IEnumerable<MissionApplication> missionApplications = _unitOfWork.MissionApplication.GetAll();
            missionVM.missionApplications = missionApplications;

            //IEnumerable<GoalMission> goalmissionDetails = _unitOfWork.
  



            var user = userDetails.FirstOrDefault(e => e.Email == emailFromSession);
            if(user != null)
            {
                missionVM.user = user;
            }
           


            if (CountryId == 0)
            {
                
                IEnumerable<City> citydetails = _unitOfWork.City.GetCityDetails();
                missionVM.Mission = missionVM.Mission;
                missionVM.City = citydetails;
            }
            else
            {

                missionVM.City = _unitOfWork.City.CityByCountry(CountryId);
                
                missionVM.particularCountry = _unitOfWork.Country.GetFirstOrDefault( e => e.CountryId == CountryId );
                missionVM.Mission = missionVM.Mission.Where(m => m.CountryId == CountryId);
            }

            missionVM.totalMissions = missionVM.Mission.Count();

            return missionVM;
        }

        /// <summary>
        /// give the filter mission if no filter is applied then gives all missions otherwise gives filtermissions
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <param name="id"></param>
        /// <param name="sessionValue"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public MissionVM ApplyFilter(string sort,string filter, long id, string sessionValue,int page= 1,string search="")
        {
            MissionVM missionObj = GetAllMissions(sessionValue, id);
            IEnumerable<Mission> missions = missionObj.Mission;
            long userid = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue).UserId;
            IEnumerable<Mission> filterMissions;
            if (filter != null)
            {
                var obj = JObject.Parse(filter);

                var cityName = obj.Value<string>("city");
                var themeName = obj.Value<string>("theme");
                var skillName = obj.Value<string>("skill");
                


                var filterCity = cityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var filterTheme = themeName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var filterSkill = skillName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


                if (filterCity.Length != 0 && filterTheme.Length != 0 && filterSkill.Length != 0)
                {
                    IEnumerable<Mission> missionsList = new List<Mission>();
                    filterMissions = missions.Where(m => m.City.Name == filterCity[0] && m.Theme.Title == filterTheme[0] && m.MissionSkills.Any(s => s.Skill.SkillName == filterSkill[0]));
                    foreach (string city in filterCity)
                    {
                        foreach (string theme in filterTheme)
                        {
                            foreach (string skill in filterSkill)
                            {
                                missionsList = missionsList.Where(m => m.Theme.Title == theme && m.City.Name == city && m.MissionSkills.Any(s => s.Skill.SkillName == skill));
                            }
                        }
                        filterMissions = filterMissions.Concat(missionsList);
                    }
                }

                else if (filterCity.Length != 0 && filterTheme.Length != 0)
                {
                    IEnumerable<Mission> missionsList = new List<Mission>();
                    filterMissions = missions.Where(m => m.City.Name == filterCity[0] && m.Theme.Title == filterTheme[0]);
                    foreach (string city in filterCity)
                    {
                        foreach (string theme in filterTheme)
                        {
                            missionsList = missionsList.Where(m => m.Theme.Title == theme && m.City.Name == city);
                        }
                        filterMissions = filterMissions.Concat(missionsList);
                    }
                }

                else if (filterTheme.Length != 0 && filterSkill.Length != 0)
                {
                    IEnumerable<Mission> missionsList = new List<Mission>();
                    filterMissions = missions.Where(m => m.MissionSkills.Any(s => s.Skill.SkillName == filterSkill[0]) && m.Theme.Title == filterTheme[0]);
                    foreach (string theme in filterTheme)
                    {
                        foreach (string skill in filterSkill)
                        {
                            missionsList = missionsList.Where(m => m.Theme.Title == theme && m.MissionSkills.Any(s => s.Skill.SkillName == skill));
                        }
                        filterMissions = filterMissions.Concat(missionsList);
                    }
                }

                else if (filterCity.Length != 0 && filterSkill.Length != 0)
                {
                    IEnumerable<Mission> missionsList = new List<Mission>();
                    filterMissions = missions.Where(m => m.MissionSkills.Any(s => s.Skill.SkillName == filterSkill[0]) && m.City.Name == filterCity[0]);
                    foreach (string city in filterCity)
                    {
                        foreach (string skill in filterSkill)
                        {
                            missionsList = missionsList.Where(m => m.City.Name == city && m.MissionSkills.Any(s => s.Skill.SkillName == skill));
                        }
                        filterMissions = filterMissions.Concat(missionsList);
                    }
                }



                else if (filterCity.Length != 0)
                {
                    filterMissions = missions.Where(u => u.City.Name == filterCity[0]);
                    foreach (string item in filterCity)
                    {
                        filterMissions = filterMissions.Concat(missions.Where(u => u.City.Name == item));
                    }
                }
                else if (filterTheme.Length != 0)
                {
                    filterMissions = missions.Where(m => m.Theme.Title == filterTheme[0]);
                    foreach (string item in filterTheme)
                    {
                        filterMissions = filterMissions.Concat(missions.Where(u => u.Theme.Title == item));
                    }
                }
                else if (filterSkill.Length != 0)
                {
                    filterMissions = missions.Where(m => m.MissionSkills.Any(s => s.Skill.SkillName == filterSkill[0]));
                    foreach (string item in filterSkill)
                    {
                        filterMissions = filterMissions.Concat(missions.Where(u => u.MissionSkills.Any(s => s.Skill.SkillName == item)));
                    }
                }
                else
                {
                    filterMissions = missions;
                }
                filterMissions = filterMissions.Distinct();
                
               /* if(searchValue != null)
                {
                    filterMissions = filterMissions.Where(mission => mission.Title.Contains(searchValue) || mission.ShortDescription.Contains(searchValue));

                }*/

                

                if (sort == "Oldest")
                {
                    filterMissions =  filterMissions.OrderBy(m => m.CreateAt).ToList();
                }
                else if (sort == "Newest")
                {
                    filterMissions = filterMissions.OrderByDescending(m => m.EndDate).ToList();
                }
                else if (sort == "Highest Available Seats")
                {
                    filterMissions = filterMissions.OrderByDescending(m => m.TotalSeats - m.MissionApplications.Count()).ToList();
                }
                else if (sort == "Lowest Available Seats")
                {
                    filterMissions = filterMissions.OrderBy(m => m.TotalSeats - m.MissionApplications.Count()).ToList();
                }
                else if (sort == "My Favourites")
                {
                    filterMissions = filterMissions.OrderByDescending(m => m.FavoriteMissions.Where(d => d.UserId == userid).Count()).ToList();
                }
                else if (sort == "Deadline")
                {
                    filterMissions = filterMissions.OrderBy(m => m.Deadline).ToList();
                }
                else
                {
                    filterMissions = filterMissions.OrderBy(m => m.MissionType).ToList();
                }
                if(search != "")
                {
                    
                    filterMissions = filterMissions.Where(mission => mission.Title.ToLower().Contains(search.ToLower()) || mission.ShortDescription.ToLower().Contains(search.ToLower()));
                }

                missionObj.totalMissions = filterMissions.Count();
                filterMissions = filterMissions.Skip((page - 1) * 9).Take(9);
                missionObj.Mission = filterMissions;
                missionObj.TotalCount = missionObj.Mission.Count();
                return missionObj;
            }


    /*        var searchValue = "dev";
            if (searchValue != null && missions != null)
            {
                missions = missions.Where(mission => mission.Title.Contains(searchValue) || (mission.ShortDescription==null)?"".Contains(searchValue):mission.ShortDescription.Contains(searchValue));

            }*/


            if (sort == "Oldest")
            {
                missions = missions.OrderBy(m => m.CreateAt).ToList();
            }
            else if (sort == "Newest")
            {
                missions = missions.OrderByDescending(m => m.EndDate).ToList();
            }
            else if (sort == "Highest Available Seats")
            {
                missions = missions.OrderByDescending(m => m.TotalSeats - m.MissionApplications.Count()).ToList();
            }
            else if (sort == "Lowest Available Seats")
            {
                missions = missions.OrderBy(m => m.TotalSeats - m.MissionApplications.Count()).ToList();
            }
            else if (sort == "My Favourites")
            {
                missions = missions.OrderByDescending(m => m.FavoriteMissions.Where(d => d.UserId == userid).Count()).ToList();
            }
            else if (sort == "Deadline")
            {
                missions = missions.OrderBy(m => m.Deadline).ToList();
            }
            else
            {
                missions = missions.OrderBy(m => m.MissionType).ToList();
            }
            if (search != "")
            {

                missions = missions.Where(mission => mission.Title.ToLower().Contains(search.ToLower()) || mission.ShortDescription.ToLower().Contains(search.ToLower()));
            }


            missionObj.totalMissions = missions.Count();
            missions = missions.Skip((page -1) * 9).Take(9);
            missionObj.Mission = missions;
            missionObj.TotalCount = missionObj.Mission.Count();
            
            return missionObj;
        }
    }

     

     


       

       

        
    }


