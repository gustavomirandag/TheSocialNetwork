using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DomainService
{
    public class FriendshipService
    {
        private IProfileRepository _profileRepository;

        public FriendshipService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public void AddFriend(Guid profileId, Guid newFriendId)
        {
            var profile = _profileRepository.Read(profileId);
            profile.Friends.Add(_profileRepository.Read(newFriendId));
            _profileRepository.Update(profile);
        }

        public IEnumerable<Profile> GetFriends(Guid profileId)
        {
            return _profileRepository.Read(profileId).Friends;
        }

        public IEnumerable<Profile> GetUnknownProfiles(Guid profileId)
        {
            var allProfiles = _profileRepository.ReadAll();
            var friends = GetFriends(profileId);
            var unknownProfiles = new List<Profile>();
            
            foreach(var profile in allProfiles)
            {
                if (!friends.Contains(profile) && profile.Id != profileId)
                    unknownProfiles.Add(profile);
            }

            return unknownProfiles;
        }
    }
}
