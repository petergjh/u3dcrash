/* 
     核心层： 控制类
*/
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public class Controller : IController
	{
        protected IView m_view;                             //IView 的引用
        protected IDictionary<string, Type> m_commandMap;   //Command 类引用的（通知名）映射
        protected static volatile IController m_instance;   //接口实例
        protected readonly object m_syncRoot = new object();//锁对象
        protected static readonly object m_staticSyncRoot = new object();//静态锁

		protected Controller()
		{
			m_commandMap = new Dictionary<string, Type>();
			InitializeController();                         //初始化
		}

        static Controller() {}

        public static IController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new Controller();
                    }
                }

                return m_instance;
            }
        }

        protected virtual void InitializeController()
        {
            m_view = View.Instance;
        }

		public virtual void ExecuteCommand(INotification note)
		{
			Type commandType = null;

			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(note.Name)) return;
				commandType = m_commandMap[note.Name];
			}

			object commandInstance = Activator.CreateInstance(commandType);

			if (commandInstance is ICommand)
			{
				((ICommand) commandInstance).Execute(note);
			}
		}

		public virtual void RegisterCommand(string notificationName, Type commandType)
		{
			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(notificationName))
				{
					m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));
				}

				m_commandMap[notificationName] = commandType;
			}
		}

		public virtual bool HasCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				return m_commandMap.ContainsKey(notificationName);
			}
		}

		public virtual void RemoveCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				if (m_commandMap.ContainsKey(notificationName))
				{
					m_view.RemoveObserver(notificationName, this);
					m_commandMap.Remove(notificationName);
				}
			}
		}
	}//Class_end
}
