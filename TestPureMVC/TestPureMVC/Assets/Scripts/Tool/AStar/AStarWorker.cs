using BaseMVCFrame.AStar;
using BaseMVCFrame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AStarWorker : MonoBehaviour
{
    /// <summary>
    /// 用到的网格
    /// </summary>
    public Grid grid;
    /// <summary>
    /// 当前所处的位置
    /// </summary>
    private Vector3Int currentPos
    {
        get
        {
            Vector3 pos = transform.position;
            return grid.WorldToCell(pos);
        }
    }
    /// <summary>
    /// 是否开始了移动
    /// </summary>
    private bool isMove;

    private void Awake()
    {
        isMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isMove)
            {
                isMove = true;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                StartCoroutine(FindPathIE(worldPos));
            }
        }
    }

    IEnumerator FindPathIE(Vector3 targetPos)
    {
        List<Vector3> worldPath = AStar.FindPath_WorldPos(transform.position,targetPos);
        StartCoroutine(MoveIE(worldPath));
        yield return new WaitForSeconds(0f);
    }


    IEnumerator MoveIE(List<Vector3> paths)
    {
        foreach (Vector3 pos in paths)
        {
            transform.DOMove(new Vector2(pos.x+0.5f,pos.y+0.5f), 1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1f);
        }
        isMove = false;
    }


    //public List<Vector2> FindPath(Vector3 targetPos)
    //{
    //    Vector3Int cellPos = grid.WorldToCell(targetPos);
    //    AStarNode startNode = MapNodeManager.Instance.GetMapNode((Vector2Int)currentPos);
    //    AStarNode endNode = MapNodeManager.Instance.GetMapNode((Vector2Int)cellPos);
    //    List<Vector2Int> paths = AStar.FindPath(startNode, endNode);
    //    foreach (Vector2 pos in paths)
    //    {
    //        Debug.Log("要移动的路径:" + pos);
    //    }

    //    List<Vector2> finalPath = new List<Vector2>();
    //    foreach (Vector2Int pos in paths)
    //    {
    //        Vector3 worldPos = grid.CellToWorld((Vector3Int)pos);

    //        finalPath.Add(new Vector2(worldPos.x+0.5f,worldPos.y+0.5f));
    //    }
    //    return finalPath;
    //}
    
}
