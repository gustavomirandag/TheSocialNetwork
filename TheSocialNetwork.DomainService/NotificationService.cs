using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DomainService
{
    public class NotificationService
    {
        private INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Notify(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public IEnumerable<Notification> GetAllNotifications(Profile recipient)
        {
            return _notificationRepository.ReadAll(recipient);
        }
    }
}
