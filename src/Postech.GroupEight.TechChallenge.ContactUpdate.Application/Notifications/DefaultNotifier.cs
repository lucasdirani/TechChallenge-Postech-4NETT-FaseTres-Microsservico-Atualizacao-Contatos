using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications
{
    public class DefaultNotifier : INotifier
    {
        private readonly List<Notification> _notifications = [];

        public IEnumerable<Notification> GetNotifications()
        {
            return _notifications.AsEnumerable();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void Handle(IEnumerable<Notification> notifications)
        {
            foreach (Notification notification in notifications)
            {
                Handle(notification);
            }
        }

        public bool HasNotification()
        {
            return _notifications.Count != 0;
        }

        public bool HasNotification(NotificationType notificationType)
        {
            return _notifications.Any(notification => notification.Type.Equals(notificationType));
        }
    }
}