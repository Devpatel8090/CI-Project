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
    public class SkillRepository : ISkillRepository
    {
        private readonly CiPlatformContext _db;

        public SkillRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Skill> GetSkillDetails()
        {
            List<Skill> skillsDetails =  _db.Skills.ToList();
            return skillsDetails;
        }
    }
}
