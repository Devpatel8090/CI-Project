using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
     public class TimesheetVM 
    {
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();

        public User user { get; set; } = new User();

        public IEnumerable<MissionApplication> LogingUserMissions { get; set; } = Enumerable.Empty<MissionApplication>();

        public IEnumerable<Timesheet> timesheets { get; set; } = Enumerable.Empty<Timesheet>();

        public Timesheet particularTimesheet { get; set; } = new Timesheet();

        public int hour { get; set; } 

        public int minute { get; set; }
        public ContactU ContactUs { get; set; } = new ContactU();

        public IEnumerable<CmsPage> CmsPages { get; set; } = Enumerable.Empty<CmsPage>();
    }
}
