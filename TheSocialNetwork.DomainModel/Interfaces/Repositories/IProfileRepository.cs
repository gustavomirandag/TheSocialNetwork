using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.DomainModel.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        void Create(Profile profile);
        Profile Read(Guid profileId);
        IEnumerable<Profile> ReadAll();
        void Update(Profile profile);
        void Delete(Guid profileId);
    }
}
