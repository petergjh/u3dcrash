/**
* 观察者数据
* 
* **/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ClientMsgCenter
{
    public class Observer
    {
        #region 变量
        /// <summary>
        /// 通知方法名称
        /// </summary>
        private string m_MessageMethod;
        /// <summary>
        /// 通知的内容
        /// </summary>
        private object m_MessageContext;
        /// <summary>
        /// 锁定
        /// </summary>
        protected readonly object m_SyncRoot = new object();
        #endregion

        #region 属性封装
        public virtual string MessageMethod { get => m_MessageMethod; private set => m_MessageMethod = value; }
        public virtual object MessageContext { get => m_MessageContext; private set => m_MessageContext = value; }
        #endregion

        #region 构造函数
        public Observer(string messageMethod, object messageContext)
        {
            m_MessageMethod = messageMethod;
            m_MessageContext = messageContext;
        }
        #endregion

        /// <summary>
        /// 调用指定的消息方法
        /// </summary>
        /// <param name="messageData"></param>
        public virtual void MessageObserver(MessageData messageData)
        {
            Debug.Log(messageData.Type);
            object context;
            string method;

            lock (m_SyncRoot)
            {
                context = MessageContext;
                method = MessageMethod;
            }
            //通过反射进行函数调用
            //得到对象类型
            Type t = context.GetType();
            BindingFlags BF = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            //得到t的方法
            MethodInfo MI = t.GetMethod(method,BF);
            //if (MI.IsNotNull())
            if (null != MI)
            {
                //进行方法调用
                MI.Invoke(context, new object[] { messageData });
            }
            else
            {
                Debug.Log("MI Is Null!");
            }
        }
        /// <summary>
        /// 对比内容是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool CompareMessageContext(object obj)
        {
            lock (m_SyncRoot)
            {
                return MessageContext.Equals(obj);
            }
        }

    }
}
