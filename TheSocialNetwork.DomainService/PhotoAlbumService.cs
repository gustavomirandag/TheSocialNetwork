using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DomainService
{
    public class PhotoAlbumService
    {
        private IPhotoAlbumRepository _photoAlbumRepository;
        private PhotoService _photoService;

        public PhotoAlbumService(IPhotoAlbumRepository photoAlbumRepository, IPhotoRepository photoRepository)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _photoService = new PhotoService(photoRepository);
        }

        public void CreatePhotoAlbum(PhotoAlbum photoAlbum)
        {
            _photoAlbumRepository.Create(photoAlbum);
        }

        public async Task AddPhotoToAlbumAsync(Photo photo, Guid photoAlbumId)
        {
            string photoUrl = await _photoService.UploadPhotoAsync(photo);
            photo.Url = photoUrl;
            PhotoAlbum photoAlbum = _photoAlbumRepository.Read(photoAlbumId);
            photoAlbum.Photos.Add(photo);
            _photoAlbumRepository.Update(photoAlbum);
        }

        public PhotoAlbum GetPhotoAlbum(Guid photoAlbumId)
        {
            return _photoAlbumRepository.Read(photoAlbumId);
        }

        public IEnumerable<PhotoAlbum> GetAllPhotoAlbums()
        {
            return _photoAlbumRepository.ReadAll();
        }

        public IEnumerable<PhotoAlbum> GetAllPhotoAlbums(Guid post)
        {
            return _photoAlbumRepository.ReadAll().Where(photoAlbum => photoAlbum.Profile.Id == post);
        }
    }
}
