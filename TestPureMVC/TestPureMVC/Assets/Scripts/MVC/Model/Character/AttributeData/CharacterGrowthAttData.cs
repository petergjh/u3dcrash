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
	public class CharacterGrowthAttData : BaseAttData
	{
        /// <summary>
        /// 初始化构造，默认所有数据为0
        /// </summary>
        public CharacterGrowthAttData() : base()
        {

        }
        /// <summary>
        /// 读取存档赋值构造
        /// </summary>
        /// <param name="m_STR"></param>
        /// <param name="m_MAG"></param>
        /// <param name="m_CON"></param>
        /// <param name="m_INT"></param>
        /// <param name="m_AGL"></param>
        /// <param name="m_AGE"></param>
        /// <param name="m_SAN"></param>
        public CharacterGrowthAttData(int m_STR, int m_MAG, int m_CON, int m_INT, int m_AGL, int m_AGE, int m_SAN) 
            : base(m_STR, m_MAG, m_CON, m_INT, m_AGL, m_AGE, m_SAN)
        {

        }

        /// <summary>
        /// 更新力量值
        /// </summary>
        /// <param name="value"></param>
        public void UpdateSTR(int value)
        {
            m_STR += value;
        }
        /// <summary>
        /// 更新魔力值
        /// </summary>
        /// <param name="value"></param>
        public void UpdateMAG(int value)
        {
            m_MAG += value;
        }
        /// <summary>
        /// 更新体力值
        /// </summary>
        /// <param name="value"></param>
        public void UpdateCON(int value)
        {
            m_CON += value;
        }
        /// <summary>
        /// 更新智力值
        /// </summary>
        /// <param name="value"></param>
        public void UpdateINT(int value)
        {
            m_INT += value;
        }
        /// <summary>
        /// 更新敏捷值
        /// </summary>
        /// <param name="value"></param>
        public void UpdateAGL(int value)
        {
            m_AGL += value;
        }
        /// <summary>
        /// 更新年龄值
        /// </summary>
        public void UpdateAGE(int value)
        {
            m_AGE += value;
        }
        /// <summary>
        /// 更新San值
        /// </summary>
        /// <param name="value"></param>
        public void UpdateSAN(int value)
        {
            m_SAN += value;
        }
        /// <summary>
        /// 年龄+1
        /// </summary>
        public void AgeGrowth()
        {
            m_AGE++;
        }
    }
}
