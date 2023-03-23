using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platfrom.Entities.Models;

namespace CI_Platfrom.Repository.Interface
{
    public interface IMissionApplicationRepository: IRepository<MissionApplication>
    {
        public List<MissionApplication> GetUsersByMissionId(long missionId);

        public List<MissionApplication> getUserMissions(long userId);
    }
}
