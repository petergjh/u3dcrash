/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.Manager
{
    /// <summary>
    /// 管理器基类
    /// </summary>
    public class BaseManager<T> where T : class, new()
    {
        /// <summary>
        /// 锁
        /// </summary>
        protected static readonly object m_StaticSyncRoot = new object();
        /// <summary>
        /// 最新的静态实例
        /// </summary>
        protected static volatile T m_instance;
        /// <summary>
        /// 静态单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_StaticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new T();
                    }
                }
                return m_instance as T;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseManager()
        {
            Initialization();
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        protected virtual void Initialization()
        {

        }
    }
}
