using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.AStar
{
    public class TileNode : AStarNode
    {
        /// <summary>
        /// 当前瓦片节点所携带的所有瓦片信息
        /// </summary>
        private List<TileInfo> tileInfos { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TileNode()
        {
            tileInfos = new List<TileInfo>();
        }

        /// <summary>
        /// 添加一个瓦片信息
        /// </summary>
        /// <param name="tilemapName"></param>
        /// <param name="tileName"></param>
        public void AddTileInfo(string tilemapName,string tileName)
        {
            if (!string.IsNullOrEmpty(tilemapName))
            {
                TileInfo tileInfo = new TileInfo(tilemapName, tileName);
                tileInfos.Add(tileInfo);
            }
        }

        /// <summary>
        /// 障碍物检查，根据具体的设计进行修改
        /// 默认判定规则：只要有一层tilemap含有障碍物就算是障碍
        /// </summary>
        /// <returns></returns>
        protected override bool ObstacleCheck()
        {
            //遍历所携带的所有瓦片信息，检查是否有障碍物
            foreach (TileInfo tileInfo in tileInfos)
            {
                //如果该层Tilemap处于活跃状态才进行障碍物检测
                if (AStarGrid.IsTileMapActive(tileInfo.tilemapName))
                {
                    //如果该层设置了瓦片
                    if (!string.IsNullOrEmpty(tileInfo.tileName))
                    {
                        //检测是在障碍物池中有该瓦片
                        if (AStarGrid.IsObstacleTile(tileInfo.tileName))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 瓦片信息
        /// </summary>
        private struct TileInfo
        {
            /// <summary>
            /// 所属的tilemap
            /// </summary>
            public string tilemapName { private set; get; }
            /// <summary>
            /// 所放置的tile，可以为""
            /// </summary>
            public string tileName { private set; get; }

            public TileInfo(string tilemapName, string tileName)
            {
                this.tilemapName = tilemapName;
                this.tileName = tileName;
            }
        }
    }

    
}

