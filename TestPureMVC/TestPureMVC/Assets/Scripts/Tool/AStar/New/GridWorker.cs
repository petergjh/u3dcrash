using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BaseMVCFrame.AStar
{
    public class GridWorker : MonoBehaviour
    {
        private void Awake()
        {
            WorkerInitialized();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void WorkerInitialized()
        {
            //设置主网格
            SetMainGrid();
            //添加障碍物瓦片
            AddObstacleTile();
            //设置Tilemap
            SetTilemap();
            //设置瓦片并创建逻辑数据
            AStarGrid.CreateTileNodes();
        }

        /// <summary>
        /// 设置主网格
        /// </summary>
        private void SetMainGrid()
        {
            //获取并设置当前网格为主网格
            Grid mainGrid = GetComponent<Grid>();
            if (mainGrid == null)
            {
                Debug.LogError("尝试获取主网格失败！");
            }
            AStarGrid.SetMainGrid(mainGrid);
        }

        /// <summary>
        /// 设置所有的Tilemap
        /// </summary>
        private void SetTilemap()
        {
            //获取所有子节点的Tilemap(包括不活跃的)
            Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>(true);
            for (int i = 0; i < tilemaps.Length; i++)
            {
                AStarGrid.AddTilemap(tilemaps[i]);
            }
        }

        /// <summary>
        /// 添加障碍物
        /// </summary>
        private void AddObstacleTile()
        {
            AStarGrid.AddObstacleTile("Lawn_4");
        }


    }
}

