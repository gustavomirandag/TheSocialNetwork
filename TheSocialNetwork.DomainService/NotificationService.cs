using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DomainService
{
    public class NotificationService
    {
        public void Notify(Profile sender, Profile recipient, string content)
        {

        }

        public IEnumerable<Notification> GetAllNotifications(Profile recipient)
        {

        }
    }
}
