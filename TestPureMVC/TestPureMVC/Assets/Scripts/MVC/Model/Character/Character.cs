/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.Data
{
    /// <summary>
    /// 角色
    /// </summary>
	public class Character 
	{
        /// <summary>
        /// 角色的随机Id
        /// </summary>
        public int _CharacterId { private set; get; }
        /// <summary>
        /// 角色的种族类型
        /// </summary>
        public RaceType _RaceType { private set; get; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string _Name { private set; get; }
        /// <summary>
        /// 阵营类型
        /// </summary>
        public CampType _CampType { private set; get; }
        /// <summary>
        /// 角色控制脚本
        /// </summary>
        public CharacterControl _CharacterControl { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="raceType"></param>
        public Character(int id, RaceType raceType,CampType campType)
        {
            _CharacterId = id;
            _RaceType = raceType;
            _CampType = campType;
            _Name = "Loli";
        }

        /// <summary>
        /// 设置种族
        /// </summary>
        public void SetRaceType(RaceType raceType)
        {
            _RaceType = raceType;
        }

        /// <summary>
        /// 获取角色的属性管理中心
        /// </summary>
        /// <returns></returns>
        public CharacterAttCenter GetCharacterAttCenter()
        {
            CharacterAttCenter attCenter = RaceGod.Instance.GetCharacterAttCenterById(_CharacterId);
            return attCenter;
        }

        /// <summary>
        /// 设置角色名称
        /// </summary>
        /// <param name="name"></param>
        public void SetCharacterName(string name)
        {
            if (name.IsNotNullAndEmpty())
            {
                _Name = name;
            }
        }
    }
}
