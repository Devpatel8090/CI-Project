using CI_Platfrom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public interface IMissionRepository : IRepository<Mission>
    {
        public List<Mission> GetMissionDetails();

        public List<Mission> GetMissionByCountry(long id);

        public Mission GetMissionByMissionId(long missionId);

        public List<Mission> getRelatedMissions(long missionId);

        //public List<Mission> GetBySort(string sort);



        //public List<Mission> GetMissionBySort(string sort);
    }
}
