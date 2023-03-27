using CI_Platfrom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public  interface IStoryRepository: IRepository<Story>
    {
        public List<Story> GetStoryDetails();

        public List<Story> getUserMissions(long userId, long missionId);
        public List<Story> getStoryById(long storyId);

        public void updateStory(Story story);
    }
}
