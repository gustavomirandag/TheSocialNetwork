using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class File
    {
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        public Stream BinaryContent { get; set; } //Imagem
        public string ContentType { get; set; }
    }
}
