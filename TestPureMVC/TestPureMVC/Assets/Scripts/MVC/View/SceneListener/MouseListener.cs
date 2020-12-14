/*******
*
*     Title:
*     Description:
*     这里封装一些常用的关于鼠标计算的变量和方法
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Data;
using BaseMVCFrame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace BaseMVCFrame
{
	public class MouseListener : MonoBehaviour
	{
        public static MouseListener _Instance;
        /// <summary>
        /// 鼠标开始拖拽时的位置
        /// </summary>
        public Vector2 MouseStartDragPos { get; private set; }
        /// <summary>
        /// 鼠标结束拖拽时的位置
        /// </summary>
        public Vector2 MouseEndDragPos { get; private set; }
        /// <summary>
        /// 是否画出鼠标的选区
        /// </summary>
        public bool IsDrawMouseChoseArea { get; private set; }
        /// <summary>
        /// 选择了的角色列表
        /// </summary>
        private List<Character> _ChosedCharacters;

        private void Awake()
        {
            _Instance = this;
            IsDrawMouseChoseArea = false;
            _ChosedCharacters = new List<Character>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_ChosedCharacters.Count>0)
                {
                    foreach (Character ct in _ChosedCharacters)
                    {
                        ct._CharacterControl.HideChosed();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("按下了一次鼠标左键");
                IsDrawMouseChoseArea = true;
                MouseStartDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MouseEndDragPos = MouseStartDragPos;
                foreach (Character ct in _ChosedCharacters)
                {
                    ct._CharacterControl.HideChosed();
                }
                _ChosedCharacters.Clear();
            }
            else if (Input.GetMouseButton(0))
            {
                MouseEndDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("按下了鼠标左键");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                float Mid_X = (MouseStartDragPos.x + MouseEndDragPos.x) * 0.5f;
                float Mid_Y = (MouseStartDragPos.y + MouseEndDragPos.y) * 0.5f;
                //检测范围内的所有被选中的角色
                Vector2 MidPos = new Vector2(Mid_X, Mid_Y);
                float HalfHeight = Mathf.Abs(MouseEndDragPos.y - MouseStartDragPos.y) * 0.5f; 
                float HalfWith = Mathf.Abs(MouseEndDragPos.x - MouseStartDragPos.x) * 0.5f;
                float Border_Left = MidPos.x - HalfWith;
                float Border_Right = MidPos.x + HalfWith;
                float Border_Top = MidPos.y + HalfHeight;
                float Border_Bottom = MidPos.y - HalfHeight;

                List<Character> characters = CreationGod.Instance.AllPlayerCharacter;
                foreach (Character ct in characters)
                {
                    if (IsChosed(ct._CharacterControl.transform, Border_Left, Border_Right, Border_Top, Border_Bottom))
                    {
                        ct._CharacterControl.ShowChosed();
                        _ChosedCharacters.Add(ct);
                    }
                }

                //Debug.Log("鼠标左键弹起");
                IsDrawMouseChoseArea = false;
            }
        }

        /// <summary>
        /// 判断是否被鼠标选择了
        /// </summary>
        /// <param name="TargetTransform"></param>
        /// <returns></returns>
        private bool IsChosed(Transform targetTransform, float Border_Left, float Border_Right, float Border_Top, float Border_Bottom)
        {
            if ((targetTransform.position.x >= Border_Left && targetTransform.position.x <= Border_Right)&&
                (targetTransform.position.y >= Border_Bottom && targetTransform.position.y <= Border_Top))
            {
                return true;
            }
            return false;
        }
        

    }
}
