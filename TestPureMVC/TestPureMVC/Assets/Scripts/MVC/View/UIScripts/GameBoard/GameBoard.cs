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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BaseMVCFrame
{
	public class GameBoard : MonoBehaviour
	{
        public static GameBoard Instance;
        /// <summary>
        /// 瓦片的尺寸
        /// </summary>
        public Vector2Int _TileSize { private set; get; }
        /// <summary>
        /// 地图的X偏移量
        /// </summary>
        public int _MapOffestX
        {
            get
            {
                return (int)(_TileSize.x * 0.5f);
            }
        }
        /// <summary>
        /// 地图的Y偏移量
        /// </summary>
        public int _MapOffestY
        {
            get
            {
                return (int)(_TileSize.y * 0.5f);
            }
        }
        /// <summary>
        /// 瓦片网格
        /// </summary>
        private Grid _grid = default;
        /// <summary>
        /// 海洋的map
        /// </summary>
        private Tilemap _Map_Sea = default;
        /// <summary>
        /// 地面的map
        /// </summary>
        private Tilemap _Map_Ground = default;
      

        TileBase[] _TileBases;

        private void Awake()
        {
            Instance = this;
            _grid = transform.GetChild<Grid>("Grid");
            _Map_Ground = _grid.transform.GetChild<Tilemap>("Map_Ground");

            Debug.Log("当前网格的名字是:"+ _Map_Ground.name);
            var bounds = _Map_Ground.cellBounds;
            _TileBases = _Map_Ground.GetTilesBlock(bounds);
            _TileSize = (Vector2Int)_Map_Ground.size;
            Debug.Log("地图格子尺寸:" + _TileSize);
            CreateMapNodeData();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos = _grid.WorldToCell(worldPos);
                TileBase tile = _Map_Ground.GetTile(cellPos);
                _Map_Ground.CellToLocal(cellPos);
                //Debug.Log("======:"+tile.name);
            }
        }

        /// <summary>
        /// 获取目标节点
        /// </summary>
        /// <returns></returns>
        public MapNode GetTargetNode()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = _grid.WorldToCell(worldPos);
            return MapNodeManager.Instance.GetMapNode((Vector2Int)cellPos);
        }

        private void CreateMapNodeData()
        {
            MapNode[,] MapNodeDatas = new MapNode[_TileSize.x, _TileSize.y];
            for (int h = 0,i = 0;h< _TileSize.y;h++)
            {
                for (int w = 0; w < _TileSize.x; w++,i++)
                {
                    MapNode mapNode = new MapNode();
                    mapNode.Map_Ground_TileName = _TileBases[i].name;
                    mapNode.SetNodePosInfo(w,h);
                    //Debug.Log("++++++++" + mapNode.Map_Ground_TileName+"--坐标:"+ mapNode.Pos);
                    MapNodeDatas[w, h] = mapNode;
                }
            }
            MapNodeManager.Instance.SetMapNodeData(MapNodeDatas);
        }
    }
}
