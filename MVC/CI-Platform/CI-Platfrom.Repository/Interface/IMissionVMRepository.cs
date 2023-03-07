using CI_Platfrom.Entities.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public interface IMissionVMRepository
    {
        public  MissionVM GetAllMissions(string emailFromSession , long CountryId, string sortby);
    }
}
