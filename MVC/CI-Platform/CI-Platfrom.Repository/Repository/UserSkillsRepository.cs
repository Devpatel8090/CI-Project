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
    public class UserSkillsRepository:Repository<UserSkill>, IUserSkillsRepository
    {
        private new readonly CiPlatformContext _db;

        public UserSkillsRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        public List<UserSkill> GetUserSkillsByUserId(long userId)
        {
            var userSkills = _db.UserSkills.Include( e => e.Skill).Where(x => x.UserId == userId).ToList();
            return userSkills;
        }
    }
}
