using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMsgCenter
{
    public class MessageHandle : IMessageHandle
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        public void RegisterMessage(string MessageName, Observer observer)
        {
            MessageCenter.Instance.RegisterObserver(MessageName, observer);
        }
        /// <summary>
        /// 处理消息
        /// </summary>
        public void SendNewMessage(string MessageName)
        {
            MessageCenter.Instance.DisposeObserver(new MessageData(MessageName));
        }
        public void SendNewMessage(string MessageName,string MessageType)
        {
            MessageCenter.Instance.DisposeObserver(new MessageData(MessageName, MessageType));
        }
        public void SendNewMessage(string MessageName,string MessageType,object MessageData)
        {
            MessageCenter.Instance.DisposeObserver(new MessageData(MessageName,MessageType, MessageData));
        }
        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="MessageName"></param>
        /// <param name="MessageContent"></param>
        public void RemoveMessage(string MessageName, object MessageContent)
        {
            MessageCenter.Instance.RemoveObserver(MessageName, MessageContent);
        }

    }
}

