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
            var newFriend = _profileRepository.Read(newFriendId);
            profile.Friends.Add(newFriend);
            newFriend.Friends.Add(profile);
            _profileRepository.Update(profile);
            _profileRepository.Update(newFriend);
        }

        public void UnFriend(Guid profileId, Guid exFriendId)
        {
            var profile = _profileRepository.Read(profileId);
            var exFriend = _profileRepository.Read(exFriendId);
            profile.Friends.Remove(exFriend);
            exFriend.Friends.Remove(profile);
            _profileRepository.Update(profile);
            _profileRepository.Update(exFriend);
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
