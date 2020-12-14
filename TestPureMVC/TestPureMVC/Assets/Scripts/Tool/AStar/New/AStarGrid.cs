using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BaseMVCFrame.AStar
{
    /// <summary>
    /// A星算法适用的网格管理
    /// </summary>
    public static class AStarGrid
    {
        #region Gird管理
        /// <summary>
        /// 主网格
        /// </summary>
        public static Grid mainGrid { private set; get; }

        /// <summary>
        /// 设置主网格
        /// </summary>
        /// <param name="grid"></param>
        public static void SetMainGrid(Grid grid)
        {
            if (grid != null)
            {
                mainGrid = grid;
            }
        }
        #endregion

        #region TileMap管理
        /// <summary>
        /// 所有的Tilemap数据
        /// </summary>
        public static List<Tilemap> tilemapsList { private set; get; }

        /// <summary>
        /// 初始化Tilemap的数据容器
        /// </summary>
        public static void InitTilemapsList()
        {
            //如果有旧数据清除旧的数据
            if (tilemapsList != null && tilemapsList.Count>0)
            {
                tilemapsList.Clear();
            }
            tilemapsList = new List<Tilemap>();
        }

        /// <summary>
        /// 添加TileMap
        /// </summary>
        /// <param name="tilemap"></param>
        public static void AddTilemap(Tilemap tilemap)
        {
            if (tilemapsList == null)
            {
                InitTilemapsList();
            }
            if (!tilemapsList.Contains(tilemap))
            {
                tilemapsList.Add(tilemap);
            }
            else
            {
                Debug.LogError("警告!重复添加相同的Tilemap数据！");
            }
        }

        /// <summary>
        /// 添加Tilemap
        /// </summary>
        /// <param name="tilemaps"></param>
        public static void AddTilemap(List<Tilemap> tilemaps)
        {
            if (tilemaps != null && tilemaps.Count > 0)
            {
                foreach (Tilemap tilemap in tilemaps)
                {
                    AddTilemap(tilemap);
                }
            }
        }

        /// <summary>
        /// --获取地图的最大尺寸--
        /// 返回所有Tilemap中的最长的X，Y值为准。
        /// 如果同时存在两个Tilemap A 和 B。
        /// A的X比B的X长，B的Y比A的Y长，则取A的X和B的Y作为Size返回。
        /// 建议所有的Tilemap尺寸保持一致。
        /// 否则可能出现未知情况。
        /// </summary>
        /// <returns></returns>
        public static Vector2Int GetMapMaxSize()
        {
            int X = 0;
            int Y = 0;
            foreach (Tilemap tilemap in tilemapsList)
            {
                Vector2Int size = GetTilemap_Vec2Size(tilemap.name);
                if (X < size.x)
                {
                    X = size.x;
                }
                if (Y < size.y)
                {
                    Y = size.y;
                }
            }
            return new Vector2Int(X, Y);
        }

        /// <summary>
        /// 获取指定的Tilemap的Vector2Int尺寸
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <returns></returns>
        public static Vector2Int GetTilemap_Vec2Size(string tilemapName)
        {
            Vector2Int Size = (Vector2Int)GetTilemap_Vec3Size(tilemapName);
            return Size;
        }

        /// <summary>
        /// 获得指定的Tilemap的Vector3Int尺寸
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <returns></returns>
        public static Vector3Int GetTilemap_Vec3Size(string tilemapName)
        {
            Tilemap tilemap = GetTileMap(tilemapName);
            if (tilemap != null)
            {
                return tilemap.size;
            }
            Debug.LogError("获取指定TileMap的尺寸失败!");
            return Vector3Int.zero;
        }

        /// <summary>
        /// 获取指定的Tilemap
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <returns></returns>
        public static Tilemap GetTileMap(string tilemapName)
        {
            if (!string.IsNullOrEmpty(tilemapName))
            {
                if (tilemapsList != null && tilemapsList.Count > 0)
                {
                    foreach (Tilemap tilemap in tilemapsList)
                    {
                        if (tilemap.name == tilemapName)
                        {
                            return tilemap;
                        }
                    }
                    Debug.LogError("查找失败!没有找到指定的Tilemap！");
                    return null;
                }
                Debug.LogError("查找失败!没有存储任何Tilemap的数据！");
                return null;
            }
            else
            {
                Debug.LogError("查找失败!要查找的Tilemap的名字不能为null或者空格！");
                return null;
            }
        }

        /// <summary>
        /// 判断指定Tilemap是否处于活跃状态
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <returns></returns>
        public static bool IsTileMapActive(string tilemapName)
        {
            Tilemap tilemap = GetTileMap(tilemapName);
            return tilemap.gameObject.activeInHierarchy;
        }

        /// <summary>
        /// 获取所有的瓦片
        /// </summary>
        /// <param name="timemapName"></param>
        /// <returns></returns>
        public static TileBase[] GetTiles(string tilemapName)
        {
            Tilemap tilemap = GetTileMap(tilemapName);
            if (tilemap != null)
            {
                var bounds = tilemap.cellBounds;
                return tilemap.GetTilesBlock(bounds);
            }
            return null;
        }

        /// <summary>
        /// 通过世界坐标获取指定的瓦片
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public static TileBase GetTile_WorldPos(string tilemapName,Vector3 worldPos)
        {
            Tilemap tilemap = GetTileMap(tilemapName);
            if (tilemap != null)
            {
                Vector3Int cellPos = mainGrid.WorldToCell(worldPos);
                return tilemap.GetTile(cellPos);
            }
            return null;
        }

        /// <summary>
        /// 通过相对坐标获取指定的瓦片
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <param name="localPos"></param>
        /// <returns></returns>
        public static TileBase GetTile_LocalPos(string tilemapName, Vector3 localPos)
        {
            Tilemap tilemap = GetTileMap(tilemapName);
            if (tilemap != null)
            {
                Vector3Int cellPos = mainGrid.LocalToCell(localPos);
                return tilemap.GetTile(cellPos);
            }
            return null;
        }
        #endregion

        #region 障碍物Tile管理
        /// <summary>
        /// 障碍物的Tile名称列表
        /// </summary>
        public static List<string> obstacleTileList { private set; get; }

        /// <summary>
        /// 初始化障碍物Tile列表
        /// </summary>
        public static void InitObstacleTileList()
        {
            //清除旧的残留数据
            if (obstacleTileList != null && obstacleTileList.Count>0)
            {
                obstacleTileList.Clear();
            }
            obstacleTileList = new List<string>();
        }

        /// <summary>
        /// 添加障碍物Tile
        /// </summary>
        /// <param name="tileName"></param>
        public static void AddObstacleTile(string tileName)
        {
            //如果存储的容器为空，初始化容器
            if (obstacleTileList == null)
            {
                InitObstacleTileList();
            }
            if (!obstacleTileList.Contains(tileName))
            {
                //如果传入的字符串为null或者空格
                if (!string.IsNullOrEmpty(tileName))
                {
                    obstacleTileList.Add(tileName);
                }
            }
            else
            {
                Debug.LogError("警告！已经添加了相同的TileName!");
            }
        }

        /// <summary>
        /// 添加一组障碍物Tile
        /// </summary>
        /// <param name="tileNames">障碍物的列表</param>
        public static void AddObstacleTile(List<string> tileNames)
        {
            //如果存储的容器为空，初始化容器
            if (obstacleTileList == null)
            {
                InitObstacleTileList();
            }
            //如果传入的字符列表不为null且有数据
            if (tileNames != null && tileNames.Count>0)
            {
                foreach (string name in tileNames)
                {
                    AddObstacleTile(name);
                }
            }
        }

        /// <summary>
        /// 移除障碍物Tile
        /// </summary>
        /// <param name="tileName"></param>
        public static bool RemoveObstacleTile(string tileName)
        {
            if (obstacleTileList != null && obstacleTileList.Count > 0)
            {
                return obstacleTileList.Remove(tileName);
            }
            return false;
        }

        /// <summary>
        /// 判断是否是障碍物Tile
        /// </summary>
        /// <param name="tileName"></param>
        /// <returns></returns>
        public static bool IsObstacleTile(string tileName)
        {
            if(obstacleTileList != null && obstacleTileList.Count > 0)
            {
                return obstacleTileList.Contains(tileName);
            }
            return false;
        }
        #endregion

        #region TileNode管理
        /// <summary>
        /// 所有的瓦片节点数据
        /// </summary>
        public static TileNode[,] tileNodes { private set; get; }

        /// <summary>
        /// 创建TileNode节点数据
        /// </summary>
        /// <param name="nodes"></param>
        public static void CreateTileNodes()
        {
            //获取所有的Tilemap
            List<Tilemap> tilemaps = tilemapsList;
            //获取地图的最大的尺寸
            Vector2Int mapSize = GetMapMaxSize();
            //计算坐标偏移量
            int offsetX = (int)(mapSize.x * 0.5f);
            int offsetY = (int)(mapSize.y * 0.5f);
            //创建瓦片节点数据
            tileNodes = new TileNode[mapSize.x, mapSize.y];
            TileBase tempTileBase = null;
            for (int h = 0; h < mapSize.y; h++)
            {
                for (int w = 0; w < mapSize.x; w++)
                {
                    TileNode tileNode = new TileNode();
                    //设置节点位置信息
                    tileNode.SetNodePosInfo(w, h);
                    //设置节点的瓦片信息
                    foreach (Tilemap tilemap in tilemaps)
                    {
                        tempTileBase = tilemap.GetTile(new Vector3Int(w - offsetX, h - offsetY, 0));
                        if (tempTileBase != null)
                        {
                            tileNode.AddTileInfo(tilemap.name, tempTileBase.name);
                        }
                    }
                    tileNodes[w, h] = tileNode;
                }
            }
        }

        /// <summary>
        /// 更新单个瓦片节点的信息
        /// </summary>
        /// <param name="pos">逻辑坐标</param>
        public static void UpdateTileNodeInfo(Vector2Int pos)
        {
            //获取所有的Tilemap
            List<Tilemap> tilemaps = tilemapsList;
            //获取地图的最大的尺寸
            Vector2Int mapSize = GetMapMaxSize();
            //计算坐标偏移量
            int offsetX = (int)(mapSize.x * 0.5f);
            int offsetY = (int)(mapSize.y * 0.5f);
            TileBase tempTileBase = null;
            TileNode tileNode = tileNodes[pos.x, pos.y];
            //设置节点的瓦片信息
            foreach (Tilemap tilemap in tilemaps)
            {
                tempTileBase = tilemap.GetTile(new Vector3Int(pos.x - offsetX, pos.y - offsetY, 0));
                if (tempTileBase != null)
                {
                    tileNode.AddTileInfo(tilemap.name, tempTileBase.name);
                }
            }
            tileNodes[pos.x, pos.y] = tileNode;
        }

        /// <summary>
        /// 更新单个瓦片节点的信息
        /// </summary>
        /// <param name="cellPos">网格坐标</param>
        public static void UpdateTileNodeInfo(Vector3Int cellPos)
        {
            //获取所有的Tilemap
            List<Tilemap> tilemaps = tilemapsList;
            //获取地图的最大的尺寸
            Vector2Int mapSize = GetMapMaxSize();
            //计算坐标偏移量
            int offsetX = (int)(mapSize.x * 0.5f);
            int offsetY = (int)(mapSize.y * 0.5f);
            TileBase tempTileBase = null;
            TileNode tileNode = tileNodes[cellPos.x + offsetX, cellPos.y+offsetY];
            //设置节点的瓦片信息
            foreach (Tilemap tilemap in tilemaps)
            {
                tempTileBase = tilemap.GetTile(cellPos);
                if (tempTileBase != null)
                {
                    tileNode.AddTileInfo(tilemap.name, tempTileBase.name);
                }
            }
            tileNodes[cellPos.x + offsetX, cellPos.y + offsetY] = tileNode;
        }

        /// <summary>
        /// 网格坐标转逻辑坐标
        /// </summary>
        /// <param name="cellPos">瓦片在网格中的坐标</param>
        /// <returns></returns>
        public static Vector2Int CellPosToTileNodePos(Vector3Int cellPos)
        {
            //获取地图的最大的尺寸
            Vector2Int mapSize = GetMapMaxSize();
            //计算坐标偏移量
            int offsetX = (int)(mapSize.x * 0.5f);
            int offsetY = (int)(mapSize.y * 0.5f);
            return new Vector2Int(cellPos.x + offsetX,cellPos.y + offsetY);
        }

        /// <summary>
        /// 获取TileNode
        /// </summary>
        /// <param name="pos">逻辑坐标</param>
        /// <returns></returns>
        public static TileNode GetTileNode(Vector2Int pos)
        {
            if (pos.x <= tileNodes.GetUpperBound(0) && pos.y<= tileNodes.GetUpperBound(1))
            {
                return tileNodes[pos.x, pos.y];
            }
            return null;
        }

        /// <summary>
        /// 获取TileNode
        /// </summary>
        /// <param name="cellPos">网格坐标</param>
        /// <returns></returns>
        public static TileNode GetTileNode(Vector3Int cellPos)
        {
            Vector2Int pos = CellPosToTileNodePos(cellPos);
            return GetTileNode(pos);
        }

        /// <summary>
        /// 获取TileNode
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        /// <returns></returns>
        public static TileNode GetTileNode(Vector3 worldPos)
        {
            Vector3Int cellPos = mainGrid.WorldToCell(worldPos);
            return GetTileNode(cellPos);
        }
        #endregion
    }
}

