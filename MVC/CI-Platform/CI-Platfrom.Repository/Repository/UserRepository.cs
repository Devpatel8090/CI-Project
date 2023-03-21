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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CiPlatformContext _db;

        public UserRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        public List<User> GetUserDetails()
        {
            List<User> usersDetails = _db.Users.ToList();
            return usersDetails;
        }

        //public List<User> GetUsersByMission(int missionId)
        //{
        //    List<User> userByMission = _db.Users.Where(m => m)
        //    return null;
        //}
    }
}
