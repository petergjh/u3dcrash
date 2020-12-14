/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Data;
using BaseMVCFrame.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.AStar
{
    public static class AStar
    {
        /// <summary>
        /// 横竖的G
        /// </summary>
        public const int RowColCostG = 10;
        /// <summary>
        /// 边缘对角的G
        /// </summary>
        public const int EdgeCostG = 14;

        /// <summary>
        /// 获得世界坐标路径
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public static List<Vector3> FindPath_WorldPos(Vector3 startPos, Vector3 endPos)
        {
            //获取节点路径
            List<Vector3Int> cellPathList = FindPath_CellPos(startPos, endPos);
            //将路径换算成世界坐标
            List<Vector3> worldPosPath = new List<Vector3>();
            foreach (Vector3Int cellPos in cellPathList)
            {
                Vector3 pos = AStarGrid.mainGrid.CellToWorld(cellPos);
                worldPosPath.Add(pos);
            }
            //将最后的坐标换成玩家的目标坐标
            worldPosPath[worldPosPath.Count - 1] = endPos;
            return worldPosPath;
        }

        /// <summary>
        /// 获得网格坐标路径
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public static List<Vector3Int> FindPath_CellPos(Vector3 startPos, Vector3 endPos)
        {
            List<Vector3Int> newPathList = new List<Vector3Int>();
            //获取网格节点路径
            List<Vector2Int> cellPosList = FindPath(startPos, endPos);
            if (cellPosList != null && cellPosList.Count>0)
            {
                foreach (Vector2Int nodePos in cellPosList)
                {
                    Vector3Int cellPos = new Vector3Int(nodePos.x, nodePos.y, 0);
                    newPathList.Add(cellPos);
                }
            }
            return newPathList;
        }

        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private static List<Vector2Int> FindPath(Vector3 startPos, Vector3 endPos)
        {
            //将世界坐标换算成网格坐标
            Vector3Int startCellPos = AStarGrid.mainGrid.WorldToCell(startPos);
            Vector3Int endCellPos = AStarGrid.mainGrid.WorldToCell(endPos);
            //获取逻辑节点
            TileNode startNode = AStarGrid.GetTileNode(startCellPos);
            TileNode endNode = AStarGrid.GetTileNode(endCellPos);
            //获得路径节点
            return FindNodePath(startNode, endNode);
        }

        /// <summary>
        /// 查找路径
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public static List<Vector2Int> FindNodePath(AStarNode startNode, AStarNode endNode)
        {
            //创建开始和结束的逻辑节点
            AStarLogicNode startLogicNode = new AStarLogicNode(startNode);
            AStarLogicNode endLogicNode = new AStarLogicNode(endNode);
            //实例化开闭的集合
            List<AStarLogicNode> openList = new List<AStarLogicNode>();
            List<AStarLogicNode> closeList = new List<AStarLogicNode>();
            //添加初始位置
            openList.Add(startLogicNode);
            //当开放列表中还有元素存在就继续
            while (openList.Count > 0)
            {
                //获取开放列表中F值最小的节点
                AStarLogicNode currentNode = GetMinF(openList);
                //将该节点从开放列表中移除
                openList.Remove(currentNode);
                //将该节点添加到关闭列表中
                closeList.Add(currentNode);

                //获得该节点周围的8个节点
                List<AStarLogicNode> SurroundNodes = GetSurroundNode(currentNode, endLogicNode);
                if (SurroundNodes.Count > 0)
                {
                    //移除掉在关闭列表中已经存储了的点和障碍物点
                    CheckHasNodeInCloseList(closeList,SurroundNodes);
                    //遍历周围的点
                    foreach (AStarLogicNode surroundNode in SurroundNodes)
                    {
                        if (IsHasNodeInOpenList(openList, surroundNode))
                        {
                            int newG = CalculateG(surroundNode.Pos, currentNode.Pos) + currentNode.G;
                            if (surroundNode.G > newG)
                            {
                                surroundNode.SetParent(currentNode, newG);
                            }
                        }
                        else
                        {
                            int G = CalculateG(surroundNode.Pos, currentNode.Pos);
                            int H = CalculateH(surroundNode.Pos, endLogicNode.Pos);
                            surroundNode.SetParent(currentNode, G, H);
                            openList.Add(surroundNode);
                        }
                    }
                    if (IsFindEndPos(openList, ref endLogicNode))
                    {
                        break;
                    }
                }
            }
            //获取路径点
            List<Vector2Int> pathNodes = new List<Vector2Int>();
            AStarLogicNode parentNode = endLogicNode;
            Vector2Int mapSize = AStarGrid.GetMapMaxSize();
            int OffsetX = (int)(mapSize.x * 0.5f);
            int OffsetY = (int)(mapSize.y * 0.5f);
            while (parentNode.Pos != startLogicNode.Pos)
            {
                pathNodes.Insert(0, new Vector2Int(parentNode.Pos.x - OffsetX, parentNode.Pos.y - OffsetY));
                parentNode = parentNode.Parent;
            }
            return pathNodes;
        }

        /// <summary>
        /// 获得列表中F值最小的节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static AStarLogicNode GetMinF(List<AStarLogicNode> nodes)
        {
            if (nodes.IsNull())
            {
                Debug.LogError("传入的获取最小值F的列表为null!");
                return null;
            }
            if (nodes.Count == 0)
            {
                Debug.LogError("传入列表中没有任何元素!");
                return null;
            }
            int min = int.MaxValue;
            AStarLogicNode node = null;
            foreach (AStarLogicNode sNode in nodes)
            {
                if (sNode.F < min)
                {
                    min = sNode.F;
                    node = sNode;
                }
            }
            return node;
        }

        /// <summary>
        /// 获取指定坐标的环绕点集合
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<AStarLogicNode> GetSurroundNode(AStarLogicNode parentNode, AStarLogicNode endNode)
        {
            int x = parentNode.X;
            int y = parentNode.Y;

            List<AStarLogicNode> SurroundNodes = new List<AStarLogicNode>();
            TileNode[,] tileNodes = AStarGrid.tileNodes; 
            //如果获取的节点数据列表为null就直接返回
            if (tileNodes == null)
            {
                return SurroundNodes;
            }
            //遍历该位置周围的8个节点，顺序从右上角开始，顺时针添加
            //x坐标小于最大值
            if (x < tileNodes.GetUpperBound(0))
            {
                //右上角
                if (y < tileNodes.GetUpperBound(1))
                {
                    CheckIsCanAdd(SurroundNodes, tileNodes[x + 1, y + 1]);
                }
                //右边
                CheckIsCanAdd(SurroundNodes, tileNodes[x + 1, y]);

                //右下角
                if (y > tileNodes.GetLowerBound(1))
                {
                    CheckIsCanAdd(SurroundNodes, tileNodes[x + 1, y - 1]);
                    //下边
                    CheckIsCanAdd(SurroundNodes, tileNodes[x, y - 1]);
                }
            }
            else if (x == tileNodes.GetUpperBound(0))
            {
                //下边
                CheckIsCanAdd(SurroundNodes, tileNodes[x, y - 1]);
            }
            //x坐标大于最小值
            if (x > tileNodes.GetLowerBound(0))
            {
                //左下角
                if (y > tileNodes.GetLowerBound(1))
                {
                    CheckIsCanAdd(SurroundNodes, tileNodes[x - 1, y - 1]);
                }
                //左边
                CheckIsCanAdd(SurroundNodes, tileNodes[x - 1, y]);

                //左上角
                if (y < tileNodes.GetUpperBound(1))
                {
                    CheckIsCanAdd(SurroundNodes, tileNodes[x - 1, y + 1]);
                    //上边
                    CheckIsCanAdd(SurroundNodes, tileNodes[x, y + 1]);
                }
            }
            else if (x == tileNodes.GetLowerBound(0))
            {
                //上边
                CheckIsCanAdd(SurroundNodes, tileNodes[x, y + 1]);
            }
            return SurroundNodes;
        }
        /// <summary>
        /// 检查是否是障碍物
        /// </summary>
        private static void CheckIsCanAdd(List<AStarLogicNode> SurroundNodes, AStarNode Node)
        {
            if (!Node.IsObstacle)
            {
                SurroundNodes.Add(new AStarLogicNode(Node));
            }
        }
        /// <summary>
        /// 检查关闭列表中是否有重复的节点并移除
        /// </summary>
        /// <param name="closeList"></param>
        /// <param name="surroundNodes"></param>
        public static void CheckHasNodeInCloseList(List<AStarLogicNode> closeList, List<AStarLogicNode> surroundNodes)
        {
            foreach (AStarLogicNode closeNode in closeList)
            {
                for (int i = surroundNodes.Count - 1; i >= 0; i--)
                {
                    if (closeNode == surroundNodes[i])
                    {
                        surroundNodes.RemoveAt(i);
                    }
                }
            }
        }
        /// <summary>
        /// 检查在开放列表中是否已经存在该节点
        /// </summary>
        /// <param name="openList"></param>
        /// <param name="Node"></param>
        /// <returns></returns>
        public static bool IsHasNodeInOpenList(List<AStarLogicNode> openList, AStarLogicNode Node)
        {
            foreach (AStarLogicNode logicNode in openList)
            {
                if (logicNode == Node)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 计算节点的H
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public static int CalculateH(Vector2Int currentPos, Vector2Int endPos)
        {
            int H = Mathf.Abs(endPos.x - currentPos.x) + Mathf.Abs(endPos.y - currentPos.y);
            return H;
        }
        /// <summary>
        /// 计算节点的G
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public static int CalculateG(Vector2Int currentPos, Vector2Int parentPos)
        {
            if (currentPos.x == parentPos.x || currentPos.y == parentPos.y)
            {
                return 10;
            }
            return 14;
        }
        /// <summary>
        /// 是否找到了终点
        /// </summary>
        /// <param name="openList"></param>
        /// <param name="endLogicNode"></param>
        /// <returns></returns>
        public static bool IsFindEndPos(List<AStarLogicNode> openList, ref AStarLogicNode endLogicNode)
        {
            foreach (AStarLogicNode logicNode in openList)
            {
                if (endLogicNode == logicNode)
                {
                    endLogicNode = logicNode;
                    return true;
                }
            }
            return false;
        }
    }


	public static class AStarOld
	{
        /// <summary>
        /// 横竖的G
        /// </summary>
        public const int RowColCostG = 10;
        /// <summary>
        /// 边缘对角的G
        /// </summary>
        public const int EdgeCostG = 14;

        public static List<Vector2Int> FindPath(AStarNode startNode,AStarNode endNode)
        {
            //创建开始和结束的逻辑节点
            AStarLogicNode startLogicNode = new AStarLogicNode(startNode);
            AStarLogicNode endLogicNode = new AStarLogicNode(endNode);
            //实例化开闭的集合
            List<AStarLogicNode> openList = new List<AStarLogicNode>();
            List<AStarLogicNode> closeList = new List<AStarLogicNode>();
            //添加初始位置
            openList.Add(startLogicNode);
            //当开放列表中还有元素存在就继续
            while (openList.Count > 0)
            {
                //获取开放列表中F值最小的节点
                AStarLogicNode currentNode = GetMinF(openList);
                //将该节点从开放列表中移除
                openList.Remove(currentNode);
                //将该节点添加到关闭列表中
                closeList.Add(currentNode);

                //获得该节点周围的8个节点
                List<AStarLogicNode> SurroundNodes = GetSurroundNode(currentNode, endLogicNode);
                if (SurroundNodes.Count>0)
                {
                    //移除掉在关闭列表中已经存储了的点和障碍物点
                    CheckHasNodeInCloseList(ref closeList, ref SurroundNodes);
                    //遍历周围的点
                    foreach (AStarLogicNode surroundNode in SurroundNodes)
                    {
                        if (IsHasNodeInOpenList(openList,surroundNode))
                        {
                            int newG = CalculateG(surroundNode.Pos, currentNode.Pos) + currentNode.G;
                            if (surroundNode.G > newG)
                            {
                                surroundNode.SetParent(currentNode,newG);
                            }
                        }
                        else
                        {
                            int G = CalculateG(surroundNode.Pos, currentNode.Pos);
                            int H = CalculateH(surroundNode.Pos, endLogicNode.Pos);
                            surroundNode.SetParent(currentNode, G, H);
                            openList.Add(surroundNode);
                        }
                    }
                    if (IsFindEndPos(openList, ref endLogicNode))
                    {
                        break;
                    }
                }
            }
            //获取路径点
            List<Vector2Int> pathNodes = new List<Vector2Int>();
            AStarLogicNode parentNode = endLogicNode;
            int OffsetX = GameBoard.Instance._MapOffestX;
            int OffsetY = GameBoard.Instance._MapOffestY;
            while (parentNode.Pos != startLogicNode.Pos)
            {
                pathNodes.Insert(0,new Vector2Int(parentNode.Pos.x-OffsetX, parentNode.Pos.y - OffsetY));
                parentNode = parentNode.Parent;
            }
            return pathNodes;
        }


        /// <summary>
        /// 获取指定坐标的环绕点集合
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<AStarLogicNode> GetSurroundNode(AStarLogicNode parentNode, AStarLogicNode endNode)
        {
            int x = parentNode.X;
            int y = parentNode.Y;

            List<AStarLogicNode> SurroundNodes = new List<AStarLogicNode>();
            MapNode[,] mapNodes = MapNodeManager.Instance.MapNodeDatas;
            //如果获取的节点数据列表为null就直接返回
            if (mapNodes.IsNull())
            {
                return SurroundNodes;
            }
            //遍历该位置周围的8个节点，顺序从右上角开始，顺时针添加
            //x坐标小于最大值
            if (x < mapNodes.GetUpperBound(0))
            {
                //右上角
                if (y < mapNodes.GetUpperBound(1))
                {
                    CheckIsCanAdd(SurroundNodes, mapNodes[x + 1, y + 1]);
                }
                //右边
                CheckIsCanAdd(SurroundNodes, mapNodes[x + 1, y]);
                //SurroundNodes.Add(new AStarLogicNode(mapNodes[x + 1, y]));

                //右下角
                if (y > mapNodes.GetLowerBound(1))
                {
                    //SurroundNodes.Add(new AStarLogicNode(mapNodes[x + 1, y - 1]));
                    CheckIsCanAdd(SurroundNodes, mapNodes[x + 1, y-1]);
                    //下边
                    CheckIsCanAdd(SurroundNodes, mapNodes[x, y - 1]);
                    //SurroundNodes.Add(new AStarLogicNode(mapNodes[x , y - 1]));
                }
            }
            else if (x == mapNodes.GetUpperBound(0))
            {
                //下边
               // SurroundNodes.Add(new AStarLogicNode(mapNodes[x, y - 1]));
                CheckIsCanAdd(SurroundNodes, mapNodes[x, y - 1]);
            }
            //x坐标大于最小值
            if (x > mapNodes.GetLowerBound(0))
            {
                //左下角
                if (y > mapNodes.GetLowerBound(1))
                {
                    //SurroundNodes.Add(new AStarLogicNode(mapNodes[x - 1, y - 1]));
                    CheckIsCanAdd(SurroundNodes, mapNodes[x - 1, y - 1]);
                }
                //左边
                CheckIsCanAdd(SurroundNodes, mapNodes[x - 1, y]);
                //SurroundNodes.Add(new AStarLogicNode(mapNodes[x - 1, y]));

                //左上角
                if (y < mapNodes.GetUpperBound(1))
                {
                    CheckIsCanAdd(SurroundNodes, mapNodes[x - 1, y + 1]);
                    //SurroundNodes.Add(new AStarLogicNode(mapNodes[x - 1, y + 1]));

                    //上边
                    CheckIsCanAdd(SurroundNodes, mapNodes[x, y + 1]);
                    //SurroundNodes.Add(new AStarLogicNode(mapNodes[x , y + 1]));
                }
            }
            else if (x == mapNodes.GetLowerBound(0))
            {
                //上边
                CheckIsCanAdd(SurroundNodes, mapNodes[x, y + 1]);
                //SurroundNodes.Add(new AStarLogicNode(mapNodes[x, y + 1]));
            }
            return SurroundNodes;
        }

        /// <summary>
        /// 检查是否是障碍物
        /// </summary>
        private static void CheckIsCanAdd(List<AStarLogicNode> SurroundNodes, AStarNode rightUpNode)
        {
            if (!rightUpNode.IsObstacle)
            {
                SurroundNodes.Add(new AStarLogicNode(rightUpNode));
            }
        }


        /// <summary>
        /// 计算节点的H
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public static int CalculateH(Vector2Int currentPos, Vector2Int endPos)
        {
            int H = Mathf.Abs(endPos.x - currentPos.x) + Mathf.Abs(endPos.y - currentPos.y);
            return H;
        }

        /// <summary>
        /// 计算节点的G
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public static int CalculateG(Vector2Int currentPos,Vector2Int parentPos)
        {
            if (currentPos.x == parentPos.x || currentPos.y == parentPos.y)
            {
                return 10;
            }
            return 14;
        }

        /// <summary>
        /// 获得列表中F值最小的节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static AStarLogicNode GetMinF(List<AStarLogicNode> nodes)
        {
            if (nodes.IsNull())
            {
                Debug.LogError("传入的获取最小值F的列表为null!");
                return null;
            }
            if (nodes.Count == 0)
            {
                Debug.LogError("传入列表中没有任何元素!");
                return null;
            }
            int min = int.MaxValue;
            AStarLogicNode node = null;
            foreach (AStarLogicNode sNode in nodes)
            {
                if (sNode.F < min)
                {
                    min = sNode.F;
                    node = sNode;
                }
            }
            return node;
        }

        /// <summary>
        /// 是否找到了终点
        /// </summary>
        /// <param name="openList"></param>
        /// <param name="endLogicNode"></param>
        /// <returns></returns>
        public static bool IsFindEndPos(List<AStarLogicNode> openList,ref AStarLogicNode endLogicNode)
        {
            foreach (AStarLogicNode logicNode in openList)
            {
                if (endLogicNode.Pos == logicNode.Pos)
                {
                    endLogicNode = logicNode;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查关闭列表中是否有重复的节点
        /// </summary>
        /// <param name="closeList"></param>
        /// <param name="surroundNodes"></param>
        public static void CheckHasNodeInCloseList(ref List<AStarLogicNode> closeList,ref List<AStarLogicNode> surroundNodes)
        {
            foreach (AStarLogicNode closeNode in closeList)
            {
                for (int i = surroundNodes.Count-1;i>=0;i--)
                {
                    if (closeNode.Pos == surroundNodes[i].Pos)
                    {
                        surroundNodes.RemoveAt(i);
                    }
                }
            }
        }

        public static bool IsHasNodeInOpenList(List<AStarLogicNode> openList, AStarLogicNode Node)
        {
            foreach (AStarLogicNode logicNode in openList)
            {
                if (logicNode.Pos == Node.Pos)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
