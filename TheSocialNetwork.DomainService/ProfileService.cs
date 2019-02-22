using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DomainService
{
    public class ProfileService
    {
        private IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public void CreateProfile(Profile profile)
        {
            _profileRepository.Create(profile);
        }

        public void UpdateProfile(Profile profile)
        {
            _profileRepository.Update(profile);
        }

        public void DeleteProfile(Guid profileId)
        {
            _profileRepository.Delete(profileId);
        }
        
        public Profile GetProfile(Guid profileId)
        {
            return _profileRepository.Read(profileId);
        }

        public IEnumerable<Profile> SearchProfileByName(string name)
        {
            return _profileRepository.ReadAll()
                .Where(p => p.Name.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Profile> SearchProfileByBirthday(DateTime birthday)
        {
            return _profileRepository.ReadAll()
                .Where(p => p.Birthday.DayOfYear.Equals(birthday.DayOfYear));
        }
    }
}
