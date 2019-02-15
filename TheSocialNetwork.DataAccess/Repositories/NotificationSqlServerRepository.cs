using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DataAccess.Repositories
{
    public class NotificationSqlServerRepository : INotificationRepository
    {
        public void Create(Notification notification)
        {
            var notificationContext = new NotificationContext();
            notificationContext.Notifications.Add(notification);
            notificationContext.SaveChanges();
        }

        public IEnumerable<Notification> ReadAll(Profile recipient)
        {
            var notificationContext = new NotificationContext();
            return notificationContext.Notifications;
        }

        private class NotificationContext : DbContext
        {
            public DbSet<Notification> Notifications { get; set; }

            public NotificationContext()
                : base(TheSocialNetwork.DataAccess.
                      Properties.Settings.Default.DbConnectionString)
            {
            }
        }
    }
}
