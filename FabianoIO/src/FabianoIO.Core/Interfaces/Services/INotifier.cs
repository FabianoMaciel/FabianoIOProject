using FabianoIO.Core.Notifications;

namespace FabianoIO.Core.Interfaces.Services
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
