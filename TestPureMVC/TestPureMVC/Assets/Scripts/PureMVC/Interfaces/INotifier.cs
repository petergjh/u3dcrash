using System;

namespace PureMVC.Interfaces
{
    public interface INotifier
    {
		void SendNotification(string notificationName);
		void SendNotification(string notificationName, object body);
		void SendNotification(string notificationName, object body, string type);
    }
}
