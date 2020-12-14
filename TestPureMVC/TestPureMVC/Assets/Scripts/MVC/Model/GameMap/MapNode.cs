/*******
*
*     Title:
*     Description:
*     地图节点数据，用来存放所有地图节点的逻辑数据
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.AStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.Data
{
	public class MapNode : AStarNode
	{
        /// <summary>
        /// 地面的瓦片数据
        /// </summary>
        public string Map_Ground_TileName { set; get; }

        /// <summary>
        /// 重写障碍物检测逻辑
        /// </summary>
        /// <returns></returns>
        protected override bool ObstacleCheck()
        {
            if (Map_Ground_TileName == "Lawn_4")
            {
                return true;
            }
            return false;
        }

    }
}
