﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class resetPassword
    {
        public string email { get; set; }
        public string token { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }
}
