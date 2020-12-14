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
using UnityEngine;

namespace BaseMVCFrame.Manager
{
	public class MapNodeManager : BaseManager<MapNodeManager>
	{
        /// <summary>
        /// 游戏节点的数据
        /// </summary>
        public MapNode[,] MapNodeDatas { private set; get; }

        protected override void Initialization()
        {
            
        }

        /// <summary>
        /// 设置地图节点数据
        /// </summary>
        /// <param name="mapNodes"></param>
        public void SetMapNodeData(MapNode[,] mapNodes)
        {
            MapNodeDatas = mapNodes;
        }

        /// <summary>
        /// 获取地图节点数据
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public MapNode GetMapNode(Vector2Int pos)
        {
            Vector2Int MapSize = GameBoard.Instance._TileSize;
            int IncrementX = (int)(MapSize.x * 0.5f);
            int IncrementY = (int)(MapSize.y * 0.5f);

            Vector2Int NewPos = new Vector2Int(pos.x+ IncrementX, pos.y+ IncrementY);

            Debug.Log("=====要查找的位置坐标:"+ NewPos);
            if (NewPos.x<= MapNodeDatas.GetUpperBound(0) && NewPos.y <= MapNodeDatas.GetUpperBound(1))
            {
                MapNode mapNode = MapNodeDatas[NewPos.x, NewPos.y];
                return mapNode;
            }
            return null;
        }
    }
}
