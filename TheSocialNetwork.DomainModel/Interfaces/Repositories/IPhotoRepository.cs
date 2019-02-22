using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DomainModel.Interfaces.Repositories
{
    public interface IPhotoRepository
    {
        string Create(Photo photo);
        Task<string> CreateAsync(Photo photo);
    }
}
