using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class Notification : EntityBase
    {
        public string Content { get; set; }
        public Profile Sender { get; set; }
        public Profile Recipient { get; set; }
        public DateTime SentDateTime { get; set; }

        public static Notification Parse(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(value);
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
