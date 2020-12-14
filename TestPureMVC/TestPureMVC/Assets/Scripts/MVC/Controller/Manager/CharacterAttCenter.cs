/*******
*
*     Title:
*           角色属性控制中心
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseMVCFrame.Data;

namespace BaseMVCFrame
{
    public class CharacterAttCenter
    {
        /// <summary>
        /// 角色基础属性数据
        /// </summary>
        private CharacterAttData m_BaseAttData;
        /// <summary>
        /// 角色成长属性数据
        /// </summary>
        private CharacterGrowthAttData m_GrowthAttData;
        /// <summary>
        /// 角色的血统
        /// </summary>
        public BloodlinesType m_Bloodline { private set; get; }

        #region 所有基础属性
        /// <summary>
        /// 力量值
        /// </summary>
        public int m_STR
        {
            get
            {
                int STR = m_BaseAttData.m_STR + m_GrowthAttData.m_STR;
                STR = STR.LessThanZeroToZero();
                return STR;
            }
        }
        /// <summary>
        /// 魔力值
        /// </summary>
        public int m_MAG
        {
            get
            {
                int MAG = m_BaseAttData.m_MAG + m_GrowthAttData.m_MAG;
                MAG = MAG.LessThanZeroToZero();
                return MAG;
            }
        }
        /// <summary>
        /// 体力值
        /// </summary>
        public int m_CON
        {
            get
            {
                int CON = m_BaseAttData.m_CON + m_GrowthAttData.m_CON;
                CON = CON.LessThanZeroToZero();
                return CON;
            }
        }
        /// <summary>
        /// 智力值
        /// </summary>
        public int m_INT
        {
            get
            {
                int INT = m_BaseAttData.m_INT + m_GrowthAttData.m_INT;
                INT = INT.LessThanZeroToZero();
                return INT;
            }
        }
        /// <summary>
        /// 敏捷值
        /// </summary>
        public int m_AGL
        {
            get
            {
                int AGL = m_BaseAttData.m_AGL + m_GrowthAttData.m_AGL;
                AGL = AGL.LessThanZeroToZero();
                return AGL;
            }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int m_AGE
        {
            get
            {
                int AGE = m_BaseAttData.m_AGE + m_GrowthAttData.m_AGE;
                AGE = AGE.LessThanZeroToZero();
                return AGE;
            }
        }
        /// <summary>
        /// 当前的SAN值
        /// </summary>
        public int m_CurrentSAN { private set; get; }
        /// <summary>
        /// SAN最大值
        /// </summary>
        public int m_MaxSAN
        {
            get
            {
                int SAN = m_BaseAttData.m_SAN + m_GrowthAttData.m_SAN;
                SAN = SAN.LessThanZeroToZero();
                return SAN;
            }
        }
        #endregion

        #region 高级属性
        /// <summary>
        /// 种族基础HP
        /// </summary>
        public int m_BaseHP
        {
            get
            {
                return m_BaseAttData.m_BaseHP;
            }
        }
        /// <summary>
        /// HP当前值
        /// </summary>
        public int m_CurrentHP { private set; get; }
        /// <summary>
        /// HP最大值
        /// </summary>
        public int m_MaxHP
        {
            get
            {
                int MaxHP = GetMaxHP();
                return MaxHP;
            }
        }
        /// <summary>
        /// MP当前值
        /// </summary>
        public int m_CurrentMP { private set; get; }
        /// <summary>
        /// MP最大值
        /// </summary>
        public int m_MaxMP
        {
            get
            {
                return GetMaxMP();
            }
        }

        /// <summary>
        /// 种族基础移动速度
        /// </summary>
        public float m_BaseMoveSpeed
        {
            get
            {
                return m_BaseAttData.m_BaseMoveSpeed;
            }
        }
        /// <summary>
        /// 当前速度值,受外部因素影响
        /// </summary>
        public float m_CurrentMoveSpeed { private set; get; }
        /// <summary>
        /// 正常速度值
        /// </summary>
        public float m_MoveSpeed
        {
            get
            {
                return GetMoveSpeed();
            }
        }
        /// <summary>
        /// 当前的攻击速度，受外部因素影响
        /// </summary>
        public float m_CurrentAttackRate { private set; get; }
        /// <summary>
        /// 正常的攻击速度值
        /// </summary>
        public float m_AttackRate
        {
            get
            {
                return GetAttackRate();
            }
        }

        /// <summary>
        /// 种族基础攻击力
        /// </summary>
        public int m_BaseAttack
        {
            get
            {
                return m_BaseAttData.m_BaseAttack;
            }
        }
        /// <summary>
        /// 当前的物理攻击力
        /// </summary>
        public int m_CurrentPhysicalAttack
        {
            get
            {
                return m_PhysicalAttack + 0;
            }
        }
        /// <summary>
        /// 物理攻击力
        /// </summary>
        public int m_PhysicalAttack
        {
            get
            {
                return GetPhysicalAttack();
            }
        }
        /// <summary>
        /// 当前的魔法攻击力
        /// </summary>
        public int m_CurrentMagicAttack
        {
            get
            {
                return m_MagicAttack + 0;
            }
        }
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int m_MagicAttack
        {
            get
            {
                return GetMagicAttack();
            }
        }

        /// <summary>
        /// 种族基础防御力
        /// </summary>
        public int m_BaseDEF
        {
            get
            {
                return m_BaseAttData.m_BaseDEF;
            }
        }
        /// <summary>
        /// 当前的物理防御值，受外部因素影响
        /// </summary>
        public int m_CurrentPhysicalDef
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// 物理防御
        /// </summary>
        public int m_PhysicalDef
        {
            get
            {
                return m_BaseDEF + 0;
            }
        }
        /// <summary>
        /// 当前的魔法防御值，受外部因素影响
        /// </summary>
        public int m_CurrentMagicDef
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// 魔法防御
        /// </summary>
        public int m_MagicDef
        {
            get
            {
                return m_BaseDEF + 0;
            }
        }

        /// <summary>
        /// 物理暴击率
        /// </summary>
        public float m_PhysicalCrit
        {
            get
            {
                return GetPhysicalCrit();
            }
        }
        /// <summary>
        /// 魔法暴击率
        /// </summary>
        public float m_MagicCrit
        {
            get
            {
                return GetMagicCrit();
            }
        }

        /// <summary>
        /// 魔法吟唱效率
        /// </summary>
        public float m_MagicSingRate
        {
            get
            {
                return GetMagicSingRate();
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public CharacterAttCenter(BloodlinesType bloodline, CharacterAttData characterAttData)
        {
            m_Bloodline = bloodline;
            m_BaseAttData = characterAttData;
            Initialization();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialization()
        {
            CreateCharacterGrowthAtt();
        }

        /// <summary>
        /// 创建角色的成长属性
        /// </summary>
        private void CreateCharacterGrowthAtt()
        {
            m_GrowthAttData = new CharacterGrowthAttData();
        }

        #region 高级属性换算公式函数
        /// <summary>
        /// 获得血量最大值
        /// </summary>
        /// <returns>返回计算后的血量值</returns>
        private int GetMaxHP()
        {
            int MaxHP = m_BaseAttData.m_BaseHP + (int)(m_STR * 0.5f) + m_CON;
            return MaxHP;
        }
        /// <summary>
        /// 获得魔力最大值
        /// </summary>
        /// <returns>返回计算后的魔力最大值</returns>
        private int GetMaxMP()
        {
            int MaxMP = m_BaseAttData.m_BaseMP + (int)(m_INT * 0.5f) + m_MAG;
            return MaxMP;
        }
        /// <summary>
        /// 获得物理攻击力
        /// </summary>
        /// <returns></returns>
        private int GetPhysicalAttack()
        {
            int Attack = m_BaseAttack + (int)(m_STR * 0.5f);
            return Attack;
        }
        /// <summary>
        /// 获得魔法攻击力
        /// </summary>
        /// <returns>返回计算后的攻击力</returns>
        private int GetMagicAttack()
        {
            int Attack = m_BaseAttack + m_MAG;
            return Attack;
        }
        /// <summary>
        /// 获得物理防御力
        /// </summary>
        /// <returns></returns>
        private int GetPhysicalDef()
        {
            int Def = m_BaseDEF + (int)(m_STR * 0.1f)+ (int)(m_CON * 0.2f);
            return Def;
        }
        /// <summary>
        /// 获得魔法防御力
        /// </summary>
        /// <returns></returns>
        private int GetMagicDef()
        {
            int Def = m_BaseDEF+ (int)(m_MAG * 0.5f);
            return Def;
        }
        /// <summary>
        /// 获得物理暴击几率
        /// </summary>
        /// <returns></returns>
        private float GetPhysicalCrit()
        {
            float Crit = m_AGL * 0.2f + m_STR*0.1f;
            if (Crit >= 50f)
            {
                Crit = 50f;
            }
            return Crit;
        }
        /// <summary>
        /// 获得魔法暴击几率
        /// </summary>
        /// <returns></returns>
        private float GetMagicCrit()
        {
            float Crit = m_INT * 0.1f + m_MAG * 0.1f;
            if (Crit >= 50f)
            {
                Crit = 50f;
            }
            return Crit;
        }
        /// <summary>
        /// 获得攻击速度
        /// </summary>
        /// <returns></returns>
        private float GetAttackRate()
        {
            float AttackRate = m_BaseAttData.m_BaseAttackRate + m_AGL * 0.01f;
            return AttackRate;
        }
        /// <summary>
        /// 获得移动速度
        /// </summary>
        /// <returns></returns>
        private float GetMoveSpeed()
        {
            float MoveSpeed = m_BaseAttData.m_BaseMoveSpeed + m_AGL * 0.01f;
            return MoveSpeed;
        }

        /// <summary>
        /// 获得魔法吟唱效率
        /// </summary>
        /// <returns></returns>
        private float GetMagicSingRate()
        {
            float SingRate = m_INT * 0.001f + m_MAG*0.001f;
            return SingRate;
        }
        #endregion

    }
}
