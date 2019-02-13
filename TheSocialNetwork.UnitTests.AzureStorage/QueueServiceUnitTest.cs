using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSocialNetwork.AzureStorageAccount;

namespace TheSocialNetwork.UnitTests.AzureStorage
{
    [TestClass]
    public class QueueServiceUnitTest
    {
        [TestMethod]
        public void EnqueueDequeueTestMethod()
        {
            //Prepare for the test
            var queueService = new QueueService();

            //Constants to enqueue and to compare
            const string queueName = "firstqueue";
            const string message = "Hello Queue!";

            //Enqueue
            queueService.Enqueue(queueName, message);

            //Dequeue
            string dequeueMessage = queueService.Dequeue(queueName);

            //Check test result
            Assert.AreEqual(message, dequeueMessage);
        }
    }
}
