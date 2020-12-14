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

namespace BaseMVCFrame
{
    /// <summary>
    /// 血统类型
    /// </summary>
    public enum BloodlinesType
    {
        /// <summary>
        /// 普通人类
        /// </summary>
        Human_Normal = 10001,
        /// <summary>
        /// 普通人类（力量特化）
        /// </summary>
        Human_Normal_STR = 10002,
        /// <summary>
        /// 普通人类（魔力特化）
        /// </summary>
        Human_Normal_MAG = 10003,
        /// <summary>
        /// 普通人类（体力特化）
        /// </summary>
        Human_Normal_CON = 10004,
        /// <summary>
        /// 普通人类（智力特化）
        /// </summary>
        Human_Normal_INT = 10005,
        /// <summary>
        /// 普通人类（敏捷特化）
        /// </summary>
        Human_Normal_AGL = 10006,
        /// <summary>
        /// 普通人类（San值特化）
        /// </summary>
        Human_Normal_San = 10007
    }

    /// <summary>
    /// 种族类型
    /// </summary>
    public enum RaceType
    {
        None,
        /// <summary>
        /// 人类
        /// </summary>
        Human = 101,
        /// <summary>
        /// 精灵
        /// </summary>
        Elf
    }
}
