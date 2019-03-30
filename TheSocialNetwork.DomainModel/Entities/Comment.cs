using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class Comment : EntityBase
    {
        public Post Post { get; set; }
        public Profile Sender { get; set; }
        public string Content { get; set; }
        public DateTime PublishDateTime { get; set; }

        public Comment()
        {
            PublishDateTime = DateTime.Now;
        }
    }
}
