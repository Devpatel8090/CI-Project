using CI_Platfrom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public interface IStoryMediaRepository : IRepository<StoryMedium>
    {
        public List<StoryMedium> GetStoryPhotoesByStoryID(long storyId);

        public List<StoryMedium> GetStoryVideosByStoryID(long storyId);

    }
}
