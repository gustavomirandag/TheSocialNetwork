using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class Post : EntityBase
    {
        public Profile Profile { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string Content { get; set; }
    }
}
