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

namespace BaseMVCFrame.Data
{
    /// <summary>
    /// 基础属性数据
    /// </summary>
	public class BaseAttData
    {
        /// <summary>
        /// 力量
        /// </summary>
        public int m_STR { protected set; get; }
        /// <summary>
        /// 魔力
        /// </summary>
        public int m_MAG { protected set; get; }
        /// <summary>
        /// 体力
        /// </summary>
        public int m_CON { protected set; get; }
        /// <summary>
        /// 智力
        /// </summary>
        public int m_INT { protected set; get; }
        /// <summary>
        /// 敏捷
        /// </summary>
        public int m_AGL { protected set; get; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int m_AGE { protected set; get; }
        /// <summary>
        /// San值
        /// </summary>
        public int m_SAN { protected set; get; }

        /// <summary>
        /// 基础属性数据
        /// </summary>
        /// <param name="m_STR">力量</param>
        /// <param name="m_MAG">魔力</param>
        /// <param name="m_CON">体力</param>
        /// <param name="m_INT">智力</param>
        /// <param name="m_AGL">敏捷</param>
        /// <param name="m_AGE">年龄</param>
        /// <param name="m_SAN">San值</param>
        public BaseAttData(int m_STR, int m_MAG, int m_CON, int m_INT, int m_AGL, int m_AGE, int m_SAN)
        {
            this.m_STR = m_STR;
            this.m_MAG = m_MAG;
            this.m_CON = m_CON;
            this.m_INT = m_INT;
            this.m_AGL = m_AGL;
            this.m_AGE = m_AGE;
            this.m_SAN = m_SAN;
        }
        /// <summary>
        /// 无参构造，默认属性值为0
        /// </summary>
        public BaseAttData()
        {
            m_STR = 0;
            m_MAG = 0;
            m_CON = 0;
            m_INT = 0;
            m_AGL = 0;
            m_AGE = 0;
            m_SAN = 0;
        }

    }
}
