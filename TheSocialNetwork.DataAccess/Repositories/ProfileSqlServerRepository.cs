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
    public class ProfileSqlServerRepository : IProfileRepository
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
            return db.Profiles.Find(profileId);
        }

        public IEnumerable<Profile> ReadAll()
        {
           return db.Profiles;
        }

        public void Update(Profile profile)
        {
            db.Entry(profile).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
