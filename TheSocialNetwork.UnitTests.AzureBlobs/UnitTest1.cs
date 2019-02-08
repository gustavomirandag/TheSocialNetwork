using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSocialNetwork.AzureStorageAccount;

namespace TheSocialNetwork.UnitTests.AzureBlobs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Preparar para o teste
            var blobService = new BlobService();

            string url = blobService.UploadImage();

        }
    }
}
