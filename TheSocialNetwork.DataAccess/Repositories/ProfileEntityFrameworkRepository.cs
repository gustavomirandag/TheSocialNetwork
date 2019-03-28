using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DataAccess.Repositories
{
    public class ProfileEntityFrameworkRepository : IProfileRepository
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        public void Create(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
        }

        public void Delete(Guid profileId)
        {
            Profile profile = Read(profileId);
            db.Profiles.Remove(profile);
            db.SaveChanges();
        }

        public Profile Read(Guid profileId)
        {
            //return db.Profiles.Find(profileId);
            return db.Profiles.Include(p => p.Friends).SingleOrDefault(p => p.Id == profileId);
        }

        public IEnumerable<Profile> ReadAll()
        {
           return db.Profiles.Include(p => p.Friends);
        }

        public void Update(Profile profile)
        {
            Profile originalProfile = db.Profiles.Find(profile.Id);
            originalProfile.Name = profile.Name;
            originalProfile.PhotoUrl = profile.PhotoUrl;
            originalProfile.Birthday = profile.Birthday;

            db.Entry(originalProfile).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
