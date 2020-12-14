/* 
    核心层： 模型类
    
*/
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;


namespace PureMVC.Core
{
    public class Model : IModel
    {
        protected static volatile IModel m_instance;        //本类实例
        protected IDictionary<string, IProxy> m_proxyMap;   //代理集合类
        protected readonly object m_syncRoot = new object();//同步锁定对象（配合Lock关键字）
        protected static readonly object m_staticSyncRoot = new object();//静态同步锁定对象

		protected Model()
		{
			m_proxyMap = new Dictionary<string, IProxy>();
			InitializeModel();
		}
        static Model(){}

        public static IModel Instance{
            get{
                if (m_instance == null){
                    lock (m_staticSyncRoot){
                        if (m_instance == null) m_instance = new Model();
                    }
                }

                return m_instance;
            }
        }

        protected virtual void InitializeModel(){}

		public virtual void RegisterProxy(IProxy proxy){
			lock (m_syncRoot){
				m_proxyMap[proxy.ProxyName] = proxy;
			}
			proxy.OnRegister();
		}

		public virtual IProxy RetrieveProxy(string proxyName){
			lock (m_syncRoot){
				if (!m_proxyMap.ContainsKey(proxyName)) return null;
				return m_proxyMap[proxyName];
			}
		}

		public virtual bool HasProxy(string proxyName){
			lock (m_syncRoot){
				return m_proxyMap.ContainsKey(proxyName);
			}
		}

		public virtual IProxy RemoveProxy(string proxyName){
			IProxy proxy = null;

			lock (m_syncRoot){
				if (m_proxyMap.ContainsKey(proxyName)){
					proxy = RetrieveProxy(proxyName);
					m_proxyMap.Remove(proxyName);
				}
			}
       
			if (proxy != null) proxy.OnRemove();
			return proxy;
		}
    }
}
