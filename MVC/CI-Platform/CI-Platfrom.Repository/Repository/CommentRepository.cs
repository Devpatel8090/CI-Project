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
    public class CommentRepository: Repository<Comment>,ICommentRepository
    {
        private readonly CiPlatformContext _db;

        public CommentRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        public  List<Comment> GetAllCommentsByMission(long id)
        {
            List<Comment> comments = _db.Comments.Include(u => u.User).Where(c => c.MissionId == id).ToList();
            return comments;
            
        }
    }
}
