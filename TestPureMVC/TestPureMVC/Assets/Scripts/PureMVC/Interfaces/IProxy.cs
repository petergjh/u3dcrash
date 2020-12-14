
using System;

namespace PureMVC.Interfaces
{
    public interface IProxy
    {
		string ProxyName { get; }
		object Data { get; set; }
		void OnRegister();
        void OnRemove();
    }
}
