using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        bool HasNotification(NotificationType notificationType);
        IEnumerable<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}