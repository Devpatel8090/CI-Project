using CI_Platfrom.Entities.Data;
using CI_Platfrom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {

        private readonly CiPlatformContext _db;

        public UnitOfWorkRepository(CiPlatformContext db)
        {
            _db = db;
            Mission = new MissionRepository(_db); ;
            City = new CityRepository(_db);
            Country = new CountryRepository(_db);
            Skill = new SkillRepository(_db);
            Theme = new ThemeRepository(_db);
            User = new UserRepository(_db);
            PasswordReset = new PasswordResetRepository(_db);
            FavoriteMission = new FavoriteMissionRepository(_db);
            MissionInvite = new MissionInviteRepository(_db);
            MissionRating = new MissionRatingRepository(_db);
            MissionMedia = new MissionMediaRepository(_db);
            Comment = new CommentRepository(_db);
            MissionApplication = new MissionApplicationRepository(_db);
            Story = new StoryRepository(_db);
            StoryInvite = new StoryInviteRepository(_db);
            StoryMedia = new StoryMediaRepository(_db);
            UserSkills = new UserSkillsRepository(_db);
            TimeSheet = new TimeSheetRepository(_db);
            ContactUs = new ContactUsRepository(_db);
            CMSPage = new CMSRepository(_db);
            Admin = new AdminRepository(_db);
            Banner = new BannerRepository(_db);
            MissionDocument = new MissionDocumentRepository(_db);
            MissionSkill = new MissionSkillRepository(_db);
        }

        public IMissionRepository Mission { get; private set; }

        public ICityRepository City { get; private set; }
        public ICountryRepository Country { get; private set; }
        public ISkillRepository Skill { get; private set; }

        public IThemeRepository Theme { get; private set; }

        public  IUserRepository User { get; private set; }

        public IPasswordResetRepository PasswordReset { get; private set; }

        public IFavoriteMissionRepository FavoriteMission { get; private set;}

        public IMissionMediaRepository MissionMedia { get; private set; }

        public IMissionInviteRepository MissionInvite { get; private set; } 

        public IMissionRatingRepository MissionRating { get; private set; }
        
        public ICommentRepository Comment { get; private set; }

        public IMissionApplicationRepository MissionApplication { get; private set; }

        public IStoryRepository Story { get; private set; }

        public IStoryInviteRepository StoryInvite { get; private set; }

        public IStoryMediaRepository StoryMedia { get; private set; }

        public IUserSkillsRepository UserSkills { get; private set; }   

        public ITimeSheetRepository TimeSheet { get; private set; }

        public IContactUsRepository ContactUs { get; private set; }

        public ICMSRepository CMSPage { get; private set; }

        public IAdminRepository Admin { get; private set; }
        public IBannerRepository Banner { get; private set; }
        public IMissionDocumentRepository MissionDocument { get; private set; } 

        public IMissionSkillRepository MissionSkill { get; private set; }
        public void save()
        {
            _db.SaveChanges();
        }
    }
}
