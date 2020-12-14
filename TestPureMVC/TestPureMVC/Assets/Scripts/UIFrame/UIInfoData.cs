/*******
*
*     Title:
*     Description:
*           UI的基础信息数据
*     Date:
*     Version:
*     Modify Recoder:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.UI
{
    public class UIInfoData
    {
        /// <summary>
        /// UI的位置类型
        /// </summary>
        public UI_PositionType PositionType { set; get; }
        /// <summary>
        /// UI的遮罩类型
        /// </summary>
        public UI_MaskType MaskType { set; get; }
        /// <summary>
        /// UI的显示类型
        /// </summary>
        public UI_ShowType ShowType { set; get; }

        public UIInfoData()
        {
            PositionType = UI_PositionType._Normal;
            MaskType = UI_MaskType._NoMask;
            ShowType = UI_ShowType._NormalUI;
        }

        public UIInfoData(UI_PositionType positionType, UI_MaskType maskType, UI_ShowType showType)
        {
            PositionType = positionType;
            MaskType = maskType;
            ShowType = showType;
        }
    }
}
