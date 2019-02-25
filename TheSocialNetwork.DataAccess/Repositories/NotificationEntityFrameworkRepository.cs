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
    public class NotificationEntityFrameworkRepository : INotificationRepository
    {
        public void Create(Notification notification)
        {
            var notificationContext = new SocialNetworkContext();
            notificationContext.Notifications.Add(notification);
            notificationContext.SaveChanges();
        }

        public IEnumerable<Notification> ReadAll(Profile recipient)
        {
            var notificationContext = new SocialNetworkContext();
            return notificationContext.Notifications;
        }
    }
}
