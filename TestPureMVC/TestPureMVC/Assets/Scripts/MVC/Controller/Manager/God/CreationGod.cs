/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseMVCFrame.Manager
{
    /// <summary>
    /// 创造神
    /// 创造世间万物
    /// </summary>
	public class CreationGod : BaseManager<CreationGod>
	{
        /// <summary>
        /// 创造角色的序号Id
        /// </summary>
        private int m_CharacterIndex;
        /// <summary>
        /// 所有创造了的角色数据字典
        /// key:角色Id value:Character
        /// </summary>
        private Dictionary<string, Character> dic_AllCharacters;
        /// <summary>
        /// 玩家的角色列表
        /// </summary>
        private Dictionary<string, Character> dic_PlayerCharacters;

        /// <summary>
        /// 获得所有玩家的角色列表
        /// </summary>
        public List<Character> AllPlayerCharacter
        {
            get
            {
                return dic_PlayerCharacters.Values.ToList();
            }
        }

        protected override void Initialization()
        {
            dic_AllCharacters = new Dictionary<string, Character>();
            dic_PlayerCharacters = new Dictionary<string, Character>();
            m_CharacterIndex = 0;
        }

        /// <summary>
        /// 创建一个角色
        /// </summary>
        /// <returns></returns>
        public Character CreateOneCharacter(CampType campType,RaceType raceType = RaceType.None)
        {
            m_CharacterIndex++;
            RaceType _RaceType = raceType;
            //如果种族是默认类型则随机抽取新的种族
            if (_RaceType == RaceType.None)
            {
                _RaceType = RaceGod.Instance.RollRace();
            }
            //生成角色数据
            Character CT = new Character(m_CharacterIndex, _RaceType, campType);
            //生成角色属性数据
            //抽取角色血统
            BloodlinesType bloodlinesType = RaceGod.Instance.RollBloodline(_RaceType);
            //根据血统生成随机属性
            RaceGod.Instance.CreateCharacterAttCenter(CT._CharacterId, _RaceType,bloodlinesType);
            return CT;
        }

        /// <summary>
        /// 标记角色，将角色记录到角色字典中
        /// </summary>
        /// <param name="CT">要记录的角色</param>
        /// <returns></returns>
        public bool MarkCharacter(Character CT)
        {
            if (!dic_AllCharacters.ContainsKey(CT._CharacterId.ToString()))
            {
                dic_AllCharacters.Add(CT._CharacterId.ToString(), CT);
                //如果角色是玩家阵容，则用玩家列表再次进行标记
                if (CT._CampType == CampType.Player)
                {
                    if (!dic_PlayerCharacters.ContainsKey(CT._CharacterId.ToString()))
                    {
                        dic_PlayerCharacters.Add(CT._CharacterId.ToString(), CT);
                        return true;
                    }
                    else
                    {
                        Debug.Log("标记失败！玩家列表中已经有相同Id的角色！");
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            Debug.Log("标记失败！角色列表中已经有相同Id的角色！");
            return false;
        }

        /// <summary>
        /// 通过角色Id查找角色数据
        /// </summary>
        /// <returns></returns>
        public Character GetCharacterById(int Id)
        {
            Character CT = null;
            dic_AllCharacters.TryGetValue(Id.ToString(),out CT);
            return CT;
        }

        public void TestShowAllCharcter()
        {
            if (dic_AllCharacters.IsNotNull() && dic_AllCharacters.Count>0)
            {
                Debug.Log("当前记录的角色数:" + dic_AllCharacters.Count);
                foreach (Character CT in dic_AllCharacters.Values)
                {
                    Debug.Log("角色ID:"+CT._CharacterId.ToString()+"名称:"+CT._Name);
                }
            }
        }
    }
}
