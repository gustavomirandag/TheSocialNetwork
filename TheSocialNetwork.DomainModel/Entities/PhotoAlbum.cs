using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class PhotoAlbum
    {
        public Guid Id { get; set; }
        public  Profile ProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public PhotoAlbum()
        {
            Id = Guid.NewGuid();
            Photos = new List<Photo>();
        }
    }
}
