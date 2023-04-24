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
    public class CityRepository : Repository<City>, ICityRepository
    
    {
        private new readonly CiPlatformContext _db;

        public CityRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }
        public List<City> GetCityDetails()
        {
            List<City> cityList = _db.Cities.ToList();
            return cityList;
        }
        public List<City> CityByCountry(long CountryId)
        {
            List<City> cityList = _db.Cities.Where(u => u.CountryId == CountryId ).ToList();
            return cityList;
        }

      


    }
}
