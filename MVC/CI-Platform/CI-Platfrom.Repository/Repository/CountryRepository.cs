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
    
    public  class CountryRepository : ICountryRepository
    {
        private readonly CiPlatformContext _db; 

        public CountryRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Country> GetCountriesDetails()
        {
            List<Country> countryList =  _db.Countries.ToList();
            return countryList;
        }
    }
}
