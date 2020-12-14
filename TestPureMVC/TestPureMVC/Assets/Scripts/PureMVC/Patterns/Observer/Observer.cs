/* 
    观察者类
*/

using System;
using System.Reflection;
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
	public class Observer : IObserver
	{
        private string m_notifyMethod;                      //通知方法名称
        private object m_notifyContext;                     //通知上下文
        protected readonly object m_syncRoot = new object();//锁定


        public virtual string NotifyMethod
        {
            private get
            {
                return m_notifyMethod;
            }
            set
            {
                m_notifyMethod = value;
            }
        }

        public virtual object NotifyContext
        {
            private get
            {
                return m_notifyContext;
            }
            set
            {
                m_notifyContext = value;
            }
        }

		public Observer(string notifyMethod, object notifyContext)
		{
			m_notifyMethod = notifyMethod;
			m_notifyContext = notifyContext;
		}

		public virtual void NotifyObserver(INotification notification)
		{
			object context;
			string method;

			lock (m_syncRoot)
			{
				context = NotifyContext;
				method = NotifyMethod;
			}

			Type t = context.GetType();
			BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
			MethodInfo mi = t.GetMethod(method, f);
			mi.Invoke(context, new object[] { notification });
		}

		public virtual bool CompareNotifyContext(object obj)
		{
			lock (m_syncRoot)
			{
				return NotifyContext.Equals(obj);
			}
		}

	}
}
