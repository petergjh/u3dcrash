
/**
 * 消息方法接口
 * 处理消息的常用方法
 * **/

namespace ClientMsgCenter
{
    public interface IMessageHandle
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        void RegisterMessage(string MessageName, Observer observer);
        /// <summary>
        /// 处理消息
        /// </summary>
        void SendNewMessage(string MessageName);
        void SendNewMessage(string MessageName, string MessageType);
        void SendNewMessage(string MessageName, string MessageType, object MessageData);
        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="MessageName"></param>
        /// <param name="MessageContent"></param>
        void RemoveMessage(string MessageName, object MessageContent);
    }
}


