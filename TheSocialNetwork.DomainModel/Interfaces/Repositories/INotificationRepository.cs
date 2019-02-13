using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DomainModel.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        void Create(Notification notification);
        IEnumerable<Notification> ReadAll(Profile recipient);
    }
}
