using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Interface
{
    public interface IUnitOfWorkRepository
    {
        public IMissionRepository Mission { get; }

        public ICityRepository City { get; }
        public ICountryRepository Country { get; }
        public  ISkillRepository Skill { get; }
        
        public  IThemeRepository Theme { get; }

        public  IUserRepository User { get; }

        public IPasswordResetRepository PasswordReset { get; }

        public IFavoriteMissionRepository FavoriteMission { get; }

        public IMissionMediaRepository MissionMedia { get; }

        public IMissionInviteRepository MissionInvite { get; }

        public IMissionRatingRepository MissionRating { get; }  

        public ICommentRepository Comment { get; }

        public IMissionApplicationRepository MissionApplication { get; }

        public IStoryRepository Story { get; }
        public IStoryInviteRepository StoryInvite { get; }

        public IStoryMediaRepository StoryMedia { get; }

        public IUserSkillsRepository UserSkills { get; }

        public ITimeSheetRepository TimeSheet { get; }

        public IContactUsRepository ContactUs { get; }
        public void save();

    }
}
