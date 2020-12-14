/***
 *  简单命令类
 * 
 */
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
		public virtual void Execute(INotification notification){}
	}
}
