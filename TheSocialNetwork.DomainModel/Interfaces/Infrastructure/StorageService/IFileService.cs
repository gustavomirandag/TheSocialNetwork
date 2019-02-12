using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Interfaces.Infrastructure.StorageService
{
    public interface IFileService
    {
        string UploadFile(string containerName, string fileName, 
            Stream file, string contentType);
    }
}
