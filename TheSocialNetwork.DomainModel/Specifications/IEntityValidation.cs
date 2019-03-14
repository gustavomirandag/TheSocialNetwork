using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.DomainModel.Specifications
{
    public interface IEntityValidation
    {
        bool IsValid();
    }
}
