using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSocialNetwork.DataAccess.Repositories;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;
using TheSocialNetwork.DomainService;

namespace TheSocialNetwork.UnitTests.ProfileServices
{
    [TestClass]
    public class ProfileServiceUnitTest
    {
        [TestMethod]
        public void ProfileServiceWithStoredProcedure()
        {
            IProfileRepository _profileEntityFrameworkRepository = new ProfileEntityFrameworkRepository();
            ProfileService _profileService = new ProfileService(_profileEntityFrameworkRepository);
            var profilesFromEntityFramework = _profileService.GetAllProfiles().ToList();

            IProfileRepository _profileStoredProcedureRepository = new ProfileStoredProcedureRepository();
            _profileService = new ProfileService(_profileStoredProcedureRepository);
            var profilesFromStoredProcedure = _profileService.GetAllProfiles().ToList();

            CollectionAssert.AreEqual(profilesFromEntityFramework, profilesFromStoredProcedure);
        }
    }
}
