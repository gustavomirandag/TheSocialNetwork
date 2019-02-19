using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DataAccess.Contexts
{
    public class SocialNetworkContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public SocialNetworkContext() 
            : base(TheSocialNetwork.DataAccess.
                  Properties.Settings.Default.DbConnectionString)
        {

        }
    }
}
