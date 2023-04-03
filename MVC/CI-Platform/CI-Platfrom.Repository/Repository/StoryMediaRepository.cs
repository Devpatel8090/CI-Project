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
    public class StoryMediaRepository: Repository<StoryMedium>,IStoryMediaRepository
    {
        private readonly CiPlatformContext _db;
        public StoryMediaRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        public List<StoryMedium> GetStoryPhotoesByStoryID(long storyId)
        {
            var storyMediaDetails = _db.StoryMedia.Where(e => e.StoryId == storyId && e.Type == "PNG" ).ToList();
            return storyMediaDetails;
        }
        public List<StoryMedium> GetStoryVideosByStoryID(long storyId)
        {
            var storyMediaDetails = _db.StoryMedia.Where(e => e.StoryId == storyId && e.Type == "URL").ToList();
            return storyMediaDetails;
        }
    }
}
