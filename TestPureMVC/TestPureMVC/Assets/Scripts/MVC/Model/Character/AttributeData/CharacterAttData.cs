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
	public class CharacterAttData : BaseAttData
	{
        /// <summary>
        /// 基础血量值
        /// </summary>
        public int m_BaseHP { private set; get; }
        /// <summary>
        /// 基础魔力值
        /// </summary>
        public int m_BaseMP { private set; get; }
        /// <summary>
        /// 基础速度值
        /// </summary>
        public float m_BaseMoveSpeed { private set; get; }
        /// <summary>
        /// 基础攻击速度
        /// </summary>
        public float m_BaseAttackRate { private set; get; }
        /// <summary>
        /// 基础攻击力
        /// </summary>
        public int m_BaseAttack { private set; get; }
        /// <summary>
        /// 基础防御力
        /// </summary>
        public int m_BaseDEF { private set; get; }

        public CharacterAttData(int m_STR, int m_MAG, int m_CON, int m_INT, int m_AGL, int m_AGE, int m_SAN)
            : base(m_STR, m_MAG, m_CON, m_INT, m_AGL, m_AGE, m_SAN)
        {
            m_BaseHP = 0;
            m_BaseMP = 0;
            m_BaseMoveSpeed = 0;
            m_BaseAttackRate = 0;
            m_BaseAttack = 0;
            m_BaseDEF = 0;
        }
        /// <summary>
        /// 初始化用
        /// </summary>
        public CharacterAttData():base(0, 0, 0, 0, 0, 0, 0)
        {
            m_BaseHP = 0;
            m_BaseMP = 0;
            m_BaseMoveSpeed = 0;
            m_BaseAttackRate = 0;
            m_BaseAttack = 0;
            m_BaseDEF = 0;
        }

        /// <summary>
        /// 设置角色种族属性数据
        /// </summary>
        /// <param name="m_BaseHP"></param>
        /// <param name="m_BaseMP"></param>
        /// <param name="m_BaseSpeed"></param>
        /// <param name="m_BaseAttackSpeed"></param>
        /// <param name="m_BaseAttack"></param>
        /// <param name="m_BaseDEF"></param>
        /// <param name="m_BaseWIS"></param>
        public void SetRaceAttData(int m_BaseHP, int m_BaseMP, float m_BaseMoveSpeed, float m_BaseAttackRate, int m_BaseAttack, int m_BaseDEF)
        {
            this.m_BaseHP = m_BaseHP;
            this.m_BaseMP = m_BaseMP;
            this.m_BaseMoveSpeed = m_BaseMoveSpeed;
            this.m_BaseAttackRate = m_BaseAttackRate;
            this.m_BaseAttack = m_BaseAttack;
            this.m_BaseDEF = m_BaseDEF;
        }
    }
}
