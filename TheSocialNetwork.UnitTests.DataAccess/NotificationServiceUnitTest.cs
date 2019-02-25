using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSocialNetwork.DataAccess.Repositories;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;
using TheSocialNetwork.DomainService;

namespace TheSocialNetwork.UnitTests.DataAccess
{
    [TestClass]
    public class NotificationServiceUnitTest
    {
        [TestMethod]
        public void NotificationSqlServerTestMethod()
        {
            //###### Prepare for the test #######

            //NotificationService
            var notificationService = new NotificationService(
                new NotificationEntityFrameworkRepository());

            //Recipient
            var recipient = new Profile
            {
                Id = Guid.Parse("826677a0-86e2-4c91-b4b4-47f48ccbbd7a"),
                Name = "Caio"
            };

            //Notification
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Content = "Hello World!",
                Sender = new Profile
                {
                    Id = Guid.Parse("c7d3461a-d03e-4d0e-9d84-f7072f499eef"),
                    Name = "Stephanie"
                },
                Recipient = recipient
            };
            //####################################

            //Execution of the test
            notificationService.Notify(notification);

            //Get All Notifications
            var notificationResponse = notificationService.GetAllNotifications(recipient).Take(1).ToArray();

            //Test result
            Assert.AreEqual(notification.Content, notificationResponse[0].Content);
        }
    }
}
