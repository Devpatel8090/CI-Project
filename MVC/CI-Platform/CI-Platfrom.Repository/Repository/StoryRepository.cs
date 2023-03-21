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

        private readonly CiPlatformContext _db;

        public StoryRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        public List<Story> GetStoryDetails()
        {
            List<Story> storyList = _db.Stories.Include(e => e.StoryMedia).ToList();
            return storyList;
        }



    }
}
