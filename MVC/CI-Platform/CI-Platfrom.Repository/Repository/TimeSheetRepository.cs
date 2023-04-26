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
    public class TimeSheetRepository: Repository<Timesheet>, ITimeSheetRepository
    {
        private new readonly CiPlatformContext _db;

        public TimeSheetRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }

        public List<Timesheet> GetTimesheetDetails()
        {
            List<Timesheet> timesheetDetails = _db.Timesheets.Include(e => e.Mission).Include(e => e.User).ToList();
            return timesheetDetails;
        }
    }
}
