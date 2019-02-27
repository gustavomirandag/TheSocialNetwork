using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DomainService
{
    public class PhotoService
    {
        private IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public string UploadPhoto(Photo photo)
        {
            return _photoRepository.Create(photo);
        }

        public async Task<string> UploadPhotoAsync(Photo photo)
        {
            return await _photoRepository.CreateAsync(photo);
        }
    }
}
