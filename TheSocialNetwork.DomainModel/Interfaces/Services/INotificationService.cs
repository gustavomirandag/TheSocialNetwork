using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DomainModel.Interfaces.Services
{
    public interface INotificationService
    {
        void Notify(Profile sender, Profile recipient, string content);
        IEnumerable<Notification> GetAllNotifications(Profile recipient);
    }
}
