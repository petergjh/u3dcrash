/* 
    接口：控制
*/
using System;

namespace PureMVC.Interfaces
{
    public interface IController
    {
        void RegisterCommand(string notificationName, Type commandType);

		void ExecuteCommand(INotification notification);

		void RemoveCommand(string notificationName);

		bool HasCommand(string notificationName);
	}
}
