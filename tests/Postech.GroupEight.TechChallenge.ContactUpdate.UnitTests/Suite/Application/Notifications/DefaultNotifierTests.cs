using FluentAssertions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Application.Notifications
{
    public class DefaultNotifierTests
    {
        [Fact(DisplayName = "Add one hundred notifications")]
        [Trait("Action", "Handle")]
        public void Handle_AddOneHundredNotifications_ShouldNotifierBeInACorrectState()
        {
            // Arrange
            DefaultNotifier notifier = new();   
            int notificationsCount = 100;      

            // Act
            for (int i = 0; i < notificationsCount; i++)
            {
                notifier.Handle(new Notification() {
                    Message = $"Notification {i + 1}",
                    Type = NotificationType.Info
                });
            }

            // Assert
            notifier.GetNotifications().Count().Should().Be(notificationsCount);
            notifier.HasNotification().Should().BeTrue();
            notifier.HasNotification(NotificationType.Info).Should().BeTrue();
        }

        [Fact(DisplayName = "Add a list of notifications")]
        [Trait("Action", "Handle")]
        public void Handle_AddListOfNotifications_ShouldNotifierBeInACorrectState()
        {
            // Arrange
            DefaultNotifier notifier = new();   
            int notificationsCount = 100;   
            IEnumerable<Notification> notifications = Enumerable.Range(1, notificationsCount).Select(i => new Notification() {
                Message = $"Notification {i}",
                Type = NotificationType.Info
            });

            // Act
            notifier.Handle(notifications);

            // Assert
            notifier.GetNotifications().Count().Should().Be(notificationsCount);
            notifier.HasNotification().Should().BeTrue();
            notifier.HasNotification(NotificationType.Info).Should().BeTrue();
        }

        [Fact(DisplayName = "No added notifications")]
        [Trait("Action", "DefaultNotifier")]
        public void Handle_NoAddedNotifications_ShouldNotifierBeInACorrectState()
        {
            // Act
            DefaultNotifier notifier = new();   

            // Assert
            notifier.GetNotifications().Should().BeEmpty();
            notifier.HasNotification().Should().BeFalse();
            notifier.HasNotification(NotificationType.Info).Should().BeFalse();
            notifier.HasNotification(NotificationType.Critical).Should().BeFalse();
            notifier.HasNotification(NotificationType.Error).Should().BeFalse();
            notifier.HasNotification(NotificationType.Warning).Should().BeFalse();
        }
    }
}