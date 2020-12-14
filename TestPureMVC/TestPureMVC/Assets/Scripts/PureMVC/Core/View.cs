/* 
    核心类： 视图层
*/
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public class View : IView
    {
        protected IDictionary<string, IMediator> m_mediatorMap;//缓存IMediator实例集合
        protected IDictionary<string, IList<IObserver>> m_observerMap;
        protected static volatile IView m_instance;    
        protected readonly object m_syncRoot = new object();
        protected static readonly object m_staticSyncRoot = new object();

		protected View()
		{
			m_mediatorMap = new Dictionary<string, IMediator>();
			m_observerMap = new Dictionary<string, IList<IObserver>>();
            InitializeView();
		}
        static View(){}

		public static IView Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new View();
					}
				}

				return m_instance;
			}
		}

        protected virtual void InitializeView(){ }

		public virtual void RegisterObserver(string notificationName, IObserver observer)
		{            
			lock (m_syncRoot)
			{
				if (!m_observerMap.ContainsKey(notificationName))
				{
					m_observerMap[notificationName] = new List<IObserver>();
				}

				m_observerMap[notificationName].Add(observer);
			}
		}

		public virtual void NotifyObservers(INotification notification)
		{
			IList<IObserver> observers = null;

			lock (m_syncRoot)
			{
				if (m_observerMap.ContainsKey(notification.Name))
				{
					IList<IObserver> observers_ref = m_observerMap[notification.Name];
					observers = new List<IObserver>(observers_ref);
				}
			}
			if (observers != null)
			{
				// Notify Observers from the working array				
				for (int i = 0; i < observers.Count; i++)
				{
					IObserver observer = observers[i];
					observer.NotifyObserver(notification);
				}
			}
		}

		public virtual void RemoveObserver(string notificationName, object notifyContext)
		{
			lock (m_syncRoot)
			{
				if (m_observerMap.ContainsKey(notificationName))
				{
					IList<IObserver> observers = m_observerMap[notificationName];

					for (int i = 0; i < observers.Count; i++)
					{
						if (observers[i].CompareNotifyContext(notifyContext))
						{
							// there can only be one Observer for a given notifyContext 
							// in any given Observer list, so remove it and break
							observers.RemoveAt(i);
							break;
						}
					}
					if (observers.Count == 0)
					{
						m_observerMap.Remove(notificationName);
					}
				}
			}
		}

		public virtual void RegisterMediator(IMediator mediator)
		{
			lock (m_syncRoot)
			{
				if (m_mediatorMap.ContainsKey(mediator.MediatorName)) return;
				m_mediatorMap[mediator.MediatorName] = mediator;

				IList<string> interests = mediator.ListNotificationInterests();

				if (interests.Count > 0)
				{
					IObserver observer = new Observer("handleNotification", mediator);
					for (int i = 0; i < interests.Count; i++)
					{
						RegisterObserver(interests[i].ToString(), observer);
					}
				}
			}
			mediator.OnRegister();
		}

		public virtual IMediator RetrieveMediator(string mediatorName)
		{
			lock (m_syncRoot)
			{
				if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
				return m_mediatorMap[mediatorName];
			}
		}

		public virtual IMediator RemoveMediator(string mediatorName)
		{
			IMediator mediator = null;

			lock (m_syncRoot)
			{
				if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
				mediator = (IMediator) m_mediatorMap[mediatorName];

				IList<string> interests = mediator.ListNotificationInterests();

				for (int i = 0; i < interests.Count; i++)
				{
					RemoveObserver(interests[i], mediator);
				}
	
				m_mediatorMap.Remove(mediatorName);
			}

			if (mediator != null) mediator.OnRemove();
			return mediator;
		}

		public virtual bool HasMediator(string mediatorName)
		{
			lock (m_syncRoot)
			{
				return m_mediatorMap.ContainsKey(mediatorName);
			}
		}

	}
}
