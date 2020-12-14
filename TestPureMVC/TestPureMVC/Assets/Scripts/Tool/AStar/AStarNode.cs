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

namespace BaseMVCFrame.AStar
{
    /// <summary>
    /// A星节点
    /// </summary>
	public class AStarNode
	{
        /// <summary>
        /// X坐标
        /// </summary>
        public int X { private set; get; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y { private set; get; }
        /// <summary>
        /// 位置信息
        /// </summary>
        public Vector2Int Pos
        {
            get
            {
                return new Vector2Int(X,Y);
            }
        }
        /// <summary>
        /// 是否是障碍物
        /// </summary>
        public bool IsObstacle
        {
            get
            {
               return ObstacleCheck();
            }
        }

        /// <summary>
        /// 设置节点坐标信息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetNodePosInfo(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// 障碍物检测函数，在对应的子类中进行重写
        /// </summary>
        /// <returns></returns>
        protected virtual bool ObstacleCheck()
        {
            return false;
        }

    }
}
