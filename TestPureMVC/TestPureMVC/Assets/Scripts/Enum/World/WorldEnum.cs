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
    /// 世界状态
    /// </summary>
    public enum WorldState
    {
        /// <summary>
        /// 运行_普通速度
        /// </summary>
        Live_Normal,
        /// <summary>
        /// 运行_两倍速
        /// </summary>
        Live_TwoSpeed,
        /// <summary>
        /// 运行_四倍速
        /// </summary>
        Live_FourSpeed,
        /// <summary>
        /// 运行_冻结时间
        /// </summary>
        Live_TimeFreeze,
        /// <summary>
        /// 运行_时间暂停
        /// </summary>
        Live_TimePause

    }
}
