﻿using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class CMSRepository: Repository<CmsPage>,ICMSRepository
    {
        private new readonly CiPlatformContext _db;

        public CMSRepository(CiPlatformContext db) : base(db)
        {
            _db = db;
        }
        public List<CmsPage> GetAllCMSPageDetails()
        {
            var cmsPage = _db.CmsPages.Where(cms => cms.DeletedAt == null).ToList();
            return cmsPage;
        }
    }
}
