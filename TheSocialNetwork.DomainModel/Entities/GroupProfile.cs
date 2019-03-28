using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class GroupProfile
    {
        public Group Group { get; set; }
        public Profile Profile { get; set; }
    }
}
