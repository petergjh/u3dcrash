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
using DG.Tweening;
using BaseMVCFrame.Data;

namespace BaseMVCFrame
{
	public class CharacterControl : MonoBehaviour
	{

        // Start is called before the first frame update
        void Start()
        {

        }

        public void MoveToTarget(Vector2 Pos)
        {
            //移动的时间
            //float MoveTime = 
        }

        /// <summary>
        /// 显示被选中
        /// </summary>
        public void ShowChosed()
        {
            transform.GetChild("ChoseCheck").Show();
        }

        public void HideChosed()
        {
            transform.GetChild("ChoseCheck").Hide();
        }

        public void MoveToPos(Vector2 targetPos)
        {
            
        }
    }
}
