using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platfrom.Entities.Models;

namespace CI_Platfrom.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public List<User> GetUserDetails(); 
    }
}
