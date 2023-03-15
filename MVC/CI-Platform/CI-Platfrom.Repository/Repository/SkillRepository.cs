using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly CiPlatformContext _db;

        public SkillRepository(CiPlatformContext db) : base(db)
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
