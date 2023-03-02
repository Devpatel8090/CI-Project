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
    public class UserRepository : IUserRepository
    {
        private readonly CiPlatformContext _db;

        public UserRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<User> GetUserDetails()
        {
            List<User> usersDetails = _db.Users.ToList();
            return usersDetails;
        }

    }
}
