/* 
    接口： 命令
*/
using System;

namespace PureMVC.Interfaces
{
    public interface ICommand
    {
		void Execute(INotification notification);
    }
}
