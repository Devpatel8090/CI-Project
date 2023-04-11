using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
     public class TimesheetVM 
    {
        public IEnumerable<User> Users { get; set; }

        public User user { get; set; }

        public IEnumerable<MissionApplication> LogingUserMissions { get; set; }

        public IEnumerable<Timesheet> timesheets { get; set; }

        public Timesheet particularTimesheet { get; set; }

        public int hour { get; set; }

        public int minute { get; set; }
        public ContactU ContactUs { get; set; }

        public IEnumerable<CmsPage> CmsPages { get; set; }
    }
}
