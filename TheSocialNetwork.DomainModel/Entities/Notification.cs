using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Profile Sender { get; set; }
        public Profile Recipient { get; set; }
        public DateTime SentDateTime { get; set; }
    }
}
