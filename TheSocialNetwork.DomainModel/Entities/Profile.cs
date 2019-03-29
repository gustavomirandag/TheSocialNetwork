using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Specifications.ProfileSpecs;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class Profile : EntityBase
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Profile> Friends { get; set; }
        public string Country { get; set; }

        public Profile()
        {
            Friends = new List<Profile>();
        }

        public override bool IsValid()
        {
            return IsNotEmptyProfileAddress.IsValid(this);
        }
    } 
}
