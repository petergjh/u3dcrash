/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Data;
using DataGod;
using ProtoData;
using System.Collections;
using System.Collections.Generic;
using tnt_deploy;
using UnityEngine;

namespace BaseMVCFrame.Manager
{
	public class RaceGod : BaseManager<RaceGod>
	{
        /// <summary>
        /// 所有角色的属性数据
        /// key:角色UID value：属性中心
        /// </summary>
        private Dictionary<string, CharacterAttCenter> dic_AllAttCenter;

        /// <summary>
        /// 初始化函数
        /// </summary>
        protected override void Initialization()
        {
            dic_AllAttCenter = new Dictionary<string, CharacterAttCenter>();
        }

        /// <summary>
        /// 通过角色Id查找角色属性
        /// </summary>
        /// <param name="CharacterId"></param>
        /// <returns></returns>
        public CharacterAttCenter GetCharacterAttCenterById(int CharacterId)
        {
            CharacterAttCenter attCenter = null;
            if (dic_AllAttCenter.IsNotNull() && dic_AllAttCenter.Count>0)
            {
                dic_AllAttCenter.TryGetValue(CharacterId.ToString(),out attCenter);
            }
            if (attCenter.IsNull())
            {
                Debug.LogError("要查找的角色数据不存在！请检查角色Id:"+ CharacterId.ToString());
            }
            return attCenter;
        }

        /// <summary>
        /// 根据角色血统生成角色的属性中心
        /// </summary>
        /// <param name="bloodlinesType"></param>
        public void CreateCharacterAttCenter(int characterId,RaceType raceType,BloodlinesType bloodlinesType)
        {
            CharacterAttData characterAttData = CreateRandomCharacterAtt(raceType,bloodlinesType);

            CharacterAttCenter attCenter = new CharacterAttCenter(bloodlinesType, characterAttData);

            dic_AllAttCenter.Add(characterId.ToString(), attCenter);
        }

        /// <summary>
        /// 抽取种族
        /// </summary>
        /// <returns></returns>
        public RaceType RollRace()
        {
            RaceType raceType = RaceType.Human;
            //List<RaceConfig> raceConfigs = DataGodTool.LoadTable(DataTableType.RaceConfig_DataTable) as List<RaceConfig>;
            //if (raceConfigs.IsNotNull() && raceConfigs.Count > 0)
            //{
            //    float Roll = Random.Range(0,100f);
            //    float RollRange = 0f;

            //    for (int i = 0;i< raceConfigs.Count;i++ )
            //    {
            //        RollRange += raceConfigs[i].Probability;
            //        if (Roll <= RollRange)
            //        {
            //            raceType = (RaceType)raceConfigs[i].Race_ID;
            //            break;
            //        }
            //    }
            //}    
            return raceType;
        }

        /// <summary>
        /// 根据种族抽取血统
        /// </summary>
        /// <param name="raceType"></param>
        /// <returns></returns>
        public BloodlinesType RollBloodline(RaceType raceType)
        {
            BloodlinesType bloodlinesType = BloodlinesType.Human_Normal;
            List<BloodlinesConfig> bloodlines = GetRaceBloodLinesByRaceType(raceType);

            if (bloodlines.IsNotNull() && bloodlines.Count > 0)
            {
                float Roll = Random.Range(0, 100f);
                float RollRange = 0f;
                foreach (BloodlinesConfig bloodline in bloodlines)
                {
                    RollRange += bloodline.Probability;
                    if (Roll <= RollRange)
                    {
                        bloodlinesType = (BloodlinesType)bloodline.Bloodline_ID;
                        Debug.Log("当前血统:"+ bloodlinesType);
                        break;
                    }
                }
            }
            return bloodlinesType;
        }

        /// <summary>
        /// 根据血统创建随机的角色属性
        /// </summary>
        /// <param name="bloodlinesType">血统类型</param>
        private CharacterAttData CreateRandomCharacterAtt(RaceType raceType,BloodlinesType bloodlinesType)
        {
            //创建血统随机属性
            BloodlinesConfig BloodLineData = GetBloodlinesAttData(bloodlinesType);
            if (BloodLineData.IsNull())
            {
                Debug.LogError("根据血统枚举查询数据失败!血统："+ bloodlinesType);
            }
            int STR = Random.Range(BloodLineData.STR_Value[0], BloodLineData.STR_Value[1] + 1);
            int MAG = Random.Range(BloodLineData.MAG_Value[0], BloodLineData.MAG_Value[1] + 1);
            int CON = Random.Range(BloodLineData.CON_Value[0], BloodLineData.CON_Value[1] + 1);
            int INT = Random.Range(BloodLineData.INT_Value[0], BloodLineData.INT_Value[1] + 1);
            int AGL = Random.Range(BloodLineData.AGL_Value[0], BloodLineData.AGL_Value[1] + 1);
            int AGE = Random.Range(BloodLineData.AGE_Value[0], BloodLineData.AGE_Value[1] + 1);
            int SAN = Random.Range(BloodLineData.SAN_Value[0], BloodLineData.SAN_Value[1] + 1);
            CharacterAttData m_BaseAttData = new CharacterAttData(STR, MAG, CON, INT, AGL, AGE, SAN);

            //创建种族随机基础属性
            RaceConfig raceConfig = GetRaceConfigByRaceType(raceType);
            if (raceConfig.IsNotNull())
            {
                int BaseHP = Random.Range(raceConfig.HP[0], raceConfig.HP[1] + 1);
                int BaseMP = Random.Range(raceConfig.MP[0], raceConfig.MP[1] + 1);
                float BaseMoveSpeed = Random.Range(raceConfig.MoveSpeed[0], raceConfig.MoveSpeed[1]+0.01f);
                float BaseAttackRate = Random.Range(raceConfig.AttackRate[0], raceConfig.AttackRate[1] + 0.01f);
                int BaseAttack = Random.Range(raceConfig.AttackValue[0],raceConfig.AttackValue[1]+1);
                int BaseDefense = Random.Range(raceConfig.DefenseValue[0], raceConfig.DefenseValue[1] + 1);
                m_BaseAttData.SetRaceAttData(BaseHP, BaseMP, BaseMoveSpeed, BaseAttackRate, BaseAttack, BaseDefense);
            }
            return m_BaseAttData;
        }

        /// <summary>
        /// 根据血统枚举获得血统配置数据
        /// </summary>
        /// <param name="bloodlinesType"></param>
        /// <returns></returns>
        private BloodlinesConfig GetBloodlinesAttData(BloodlinesType bloodlinesType)
        {
            List<BloodlinesConfig> bloodlinesConfigs = DataGodTool.LoadTable(DataTableType.BloodlinesConfig_DataTable) as List<BloodlinesConfig>;

            foreach (BloodlinesConfig configData in bloodlinesConfigs)
            {
                if (configData.Bloodline_ID == (int)bloodlinesType)
                {
                    return configData;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过种族获取该种族的所有血统
        /// </summary>
        /// <param name="raceType"></param>
        /// <returns></returns>
        private List<BloodlinesConfig> GetRaceBloodLinesByRaceType(RaceType raceType)
        {
            List<BloodlinesConfig> bloodlines = new List<BloodlinesConfig>();
            //获取血统数据
            List<BloodlinesConfig> bloodlinesConfigs = DataGodTool.LoadTable(DataTableType.BloodlinesConfig_DataTable) as List<BloodlinesConfig>;
            if (bloodlinesConfigs.IsNotNull() && bloodlinesConfigs.Count > 0)
            {
                foreach (BloodlinesConfig bloodline in bloodlinesConfigs)
                {
                    if (bloodline.Race_ID == (int)raceType)
                    {
                        bloodlines.Add(bloodline);
                    }
                }
            }
            return bloodlines;
        }

        /// <summary>
        /// 通过种族类型获取种族配置数据
        /// </summary>
        /// <param name="raceType"></param>
        /// <returns></returns>
        private RaceConfig GetRaceConfigByRaceType(RaceType raceType)
        {
            List<RaceConfig> raceConfigs = DataGodTool.LoadTable(DataTableType.RaceConfig_DataTable) as List<RaceConfig>;
            foreach (RaceConfig raceConfig in raceConfigs)
            {
                if (raceConfig.Race_ID == (int)raceType)
                {
                    return raceConfig;
                }
            }
            return null;
        }

    }
}
