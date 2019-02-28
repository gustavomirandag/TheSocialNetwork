using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.UnitTests.ProfileWebAPI
{
    [TestClass]
    public class ProfileWebApiTest
    {
        [TestMethod]
        public void ProfileWebApiTestMethod()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://thesocialnetworkprofilewebapi20190227095906.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //HTTP Get
            Task<HttpResponseMessage> response2 = client.GetAsync("api/profiles");
            string serializedProfilesCollection = response2.Result.Content.ReadAsStringAsync().Result;
            Profile[] profiles = Newtonsoft
                .Json.JsonConvert
                .DeserializeObject<Profile[]>(serializedProfilesCollection);

            foreach(var profile in profiles)
                Debug.WriteLine(profile.Name);

            Assert.AreEqual(profiles.Length, 3);
        }
    }
}
