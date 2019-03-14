using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class File : EntityBase
    {
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public Stream BinaryContent { get; set; } //Imagem (não armazena no BD)
        public string Url { get; set; }
        public string ContentType { get; set; }

        public File()
        {
            Id = Guid.NewGuid();
        }
    }
}
