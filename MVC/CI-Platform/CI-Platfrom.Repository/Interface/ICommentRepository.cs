using CI_Platfrom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public interface ICommentRepository: IRepository<Comment>
    {
    

       public  List<Comment> GetAllCommentsByMission(long id);
       public  List<Comment> GetAllComments();
    }
}
