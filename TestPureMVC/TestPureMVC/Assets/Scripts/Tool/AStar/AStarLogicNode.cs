using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.AStar
{
    /// <summary>
    /// AStar的逻辑运算节点
    /// </summary>
    public class AStarLogicNode
    {
        /// <summary>
        /// x坐标
        /// </summary>
        public int X { private set; get; }
        /// <summary>
        /// y坐标
        /// </summary>
        public int Y { private set; get; }
        /// <summary>
        /// 位置坐标
        /// </summary>
        public Vector2Int Pos
        {
            get
            {
                return new Vector2Int(X,Y);
            }
        }

        public int F { private set; get; }

        public int G { private set; get; }

        public int H { private set; get; }
        /// <summary>
        /// 是否是障碍物
        /// </summary>
        public bool IsObstacle { private set; get; }
        /// <summary>
        /// 逻辑父节点
        /// </summary>
        public AStarLogicNode Parent { private set; get; }

        /// <summary>
        /// 构造函数，将AStarNode转换成AStarLogicNode
        /// </summary>
        /// <param name="node"></param>
        public AStarLogicNode(AStarNode node)
        {
            X = node.X;
            Y = node.Y;
            IsObstacle = node.IsObstacle;
            F = 0;
            G = 0;
            H = 0;
            Parent = null;
        }

        /// <summary>
        /// 设置位置信息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPos(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        public void SetParent(AStarLogicNode parent,int g,int h)
        {
            Parent = parent;
            G = parent.G + g;
            H = h;
            F = G + H;
        }

        public void SetParent(AStarLogicNode parent, int g)
        {
            Parent = parent;
            G = parent.G + g;
            F = G + H;
        }

        #region 重载 == != 运算符
        public static bool operator == (AStarLogicNode aNode, AStarLogicNode bNode)
        {
            return aNode.Pos == bNode.Pos;
        }
        public static bool operator !=(AStarLogicNode aNode, AStarLogicNode bNode)
        {
            return aNode.Pos != bNode.Pos;
        }
        #endregion
    }
}

