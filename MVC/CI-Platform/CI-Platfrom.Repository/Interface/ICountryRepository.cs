using CI_Platfrom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public interface ICountryRepository : IRepository<Country>
    {
        public List<Country> GetCountriesDetails();

        public List<Country> GetAllCountries(long countryid);
    }
}
