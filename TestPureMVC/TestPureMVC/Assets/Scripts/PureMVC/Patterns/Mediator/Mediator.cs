/***
 * 视图类 
 * 
 */
using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    public class Mediator : Notifier, IMediator, INotifier
	{
        protected string m_mediatorName;
        protected object m_viewComponent;
		public const string NAME = "Mediator";


        public Mediator()
            : this(NAME, null){}

        public Mediator(string mediatorName)
            : this(mediatorName, null){}

		public Mediator(string mediatorName, object viewComponent)
		{
			m_mediatorName = (mediatorName != null) ? mediatorName : NAME;
			m_viewComponent = viewComponent;
		}
		public virtual IList<string> ListNotificationInterests()
		{
			return new List<string>();
		}

		public virtual void HandleNotification(INotification notification){}

		public virtual void OnRegister(){}

		public virtual void OnRemove(){}

		public virtual string MediatorName
		{
			get { return m_mediatorName; }
		}

		public virtual object ViewComponent
		{
			get { return m_viewComponent; }
			set { m_viewComponent = value; }
		}
	}
}
