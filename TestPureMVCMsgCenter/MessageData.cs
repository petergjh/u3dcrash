using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMsgCenter
{
    /// <summary>
    /// 消息载体类
    /// </summary>
    public class MessageData 
    {
        #region 变量
        /// <summary>
        /// 消息名
        /// </summary>
        private string m_Name;
        /// <summary>
        /// 消息的种类
        /// </summary>
        private string m_Type;
        /// <summary>
        /// 消息的内容
        /// </summary>
        private object m_Data;
        #endregion

        #region 属性封装
        public virtual string Name { private set { m_Name = value; }get { return m_Name; } }
        public virtual string Type { private set { m_Type = value; } get { return m_Type; } }
        public virtual object Data { private set { m_Data = value; } get { return m_Data; } }
        #endregion

        #region 构造函数
        public MessageData(string name) 
        {
            m_Name = name;
            m_Type = null;
            m_Data = null;
        }
        public MessageData(string name, string type) 
        {
            m_Name = name;
            m_Type = type;
            m_Data = null;
        }
        public MessageData(string name, string type, object data)
        {
            m_Name = name;
            m_Type = type;
            m_Data = data;
        }
        #endregion
    }
}
