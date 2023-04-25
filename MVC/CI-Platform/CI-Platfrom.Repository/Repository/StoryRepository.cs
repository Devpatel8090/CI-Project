using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {

        private new readonly CiPlatformContext _db;

        public StoryRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }
        public List<Story> GetStoryDetails()
        {
            List<Story> storyList = _db.Stories.Include(e => e.Mission).Include(e => e.StoryInvites).Include(e => e.User).Include(e => e.StoryMedia).ToList();
            return storyList;
        }
        public List<Story> GetPublishedStoryDetails()
        {
            List<Story> storyList = _db.Stories.Where(e => e.Status == "PUBLISHED" && e.DeletedAt == null).Include(e => e.StoryMedia).ToList();
            return storyList;
        }

        public List<Story> getStoryByMissionId(long missionId)
        {
            List<Story> storyDetails = _db.Stories.Include(e => e.Mission).Include(e => e.StoryInvites).Include(e => e.User).Include(e => e.StoryMedia).ToList();
            return storyDetails;
        }

        public List<Story> getUserMissions(long userId,long missionId)
        {
            List<Story> storyDetails = _db.Stories.Include(e => e.Mission).Include(e => e.User).Include(e => e.StoryMedia).Where(e => e.UserId == userId && e.MissionId== missionId).ToList();
            return storyDetails;
        }

        public Story getStoryById(long storyId)
        {
            Story ?storyDetails = _db.Stories.Include(e => e.Mission).Include(e => e.User).Include(e => e.StoryMedia).FirstOrDefault(e => e.StoryId == storyId  );
            return storyDetails?? new();
        }

        public void updateStory(Story story)
        {
            var model = _db.Entry(story);
            model.State = EntityState.Modified;
            _db.SaveChanges();
        }


    }
}
