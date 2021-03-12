/*
*	Title:消息管理中心
*	Description:该类主要管理UI消息和数据消息之间的传递以及调用
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMsgCenter
{
	public class MessageCenter
	{
        /// <summary>
        /// 消息数据的容器
        /// </summary>
        protected Dictionary<string, List<Observer>> dic_MessageObserver;
        /// <summary>
        /// 锁
        /// </summary>
        protected static readonly object m_StaticSyncRoot = new object();
        /// <summary>
        /// 最新的静态实例
        /// </summary>
        protected static volatile MessageCenter m_instance;
        /// <summary>
        /// 静态单例
        /// </summary>
        public static MessageCenter Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_StaticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new MessageCenter();
                    }
                }
                return m_instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageCenter()
        {
            dic_MessageObserver = new Dictionary<string, List<Observer>>();
        }
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static MessageCenter() { }


        /// <summary>
        /// 注册观察者
        /// </summary>
        /// <param name="MessageName"></param>
        public virtual void RegisterObserver(string MessageName,Observer observer)
        {
            lock (m_StaticSyncRoot)
            {
                if (!dic_MessageObserver.ContainsKey(MessageName))
                {
                    dic_MessageObserver[MessageName] = new List<Observer>();
                }
                dic_MessageObserver[MessageName].Add(observer);
                Debug.Log("Messagename = "+ MessageName + "  observer = "+ observer.MessageContext);
                Debug.Log("Messagename = "+ MessageName + "  observerList数量 = "+dic_MessageObserver.Count);
            }
        }

        /// <summary>
        /// 取消观察者
        /// </summary>
        /// <param name="MessageName"></param>
        /// <param name="observer"></param>
        public virtual void UnRegisterObserver(string MessageName)
        {
             lock (m_StaticSyncRoot)
            {
                if (!dic_MessageObserver.ContainsKey(MessageName))
                {
                    dic_MessageObserver[MessageName].Clear();
                }
                Debug.Log("取消Messagename = "+ MessageName  );
            }
        }
        /// <summary>
        /// 执行观察者消息
        /// </summary>
        /// <param name="messageData"></param>
        public virtual void DisposeObserver(MessageData messageData)
        {
            Debug.Log("messageData.Name ="+ messageData.Name);
            List<Observer> observers = null;
            lock (m_StaticSyncRoot)
            {
                if (dic_MessageObserver.ContainsKey(messageData.Name))
                {
                    List<Observer> observers_ref = dic_MessageObserver[messageData.Name];
                    observers = new List<Observer>(observers_ref);
                }
            }
            if (observers != null)
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    Observer observer = observers[i];
                    //Debug.Log("执行了方法");
                    observer.MessageObserver(messageData);
                }
            }
            else
            {
                Debug.Log("observers为空");
            }
        }
        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="MessageName"></param>
        /// <param name="MessageContent"></param>
        public virtual void RemoveObserver(string MessageName, object MessageContent)
        {
            lock (m_StaticSyncRoot)
            {
                if (dic_MessageObserver.ContainsKey(MessageName))
                {
                    List<Observer> observers = dic_MessageObserver[MessageName];

                    for (int i = 0; i < observers.Count; i++)
                    {
                        if (observers[i].CompareMessageContext(MessageContent))
                        {
                            observers.RemoveAt(i);
                            break;
                        }
                    }
                    if (observers.Count == 0)
                    {
                        dic_MessageObserver.Remove(MessageName);
                    }
                }
            }
        }


    }//Class_end
}