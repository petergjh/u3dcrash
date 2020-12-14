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
using BaseMVCFrame;

namespace BaseMVCFrame.UI
{
	public class RootUI : BaseUI
	{

        #region 声明UI节点层
        /// <summary>
        /// 背景层
        /// </summary>
        public Transform _Background { private set; get; }
        /// <summary>
        /// 普通层
        /// </summary>
        public Transform _Normal { private set; get; }
        /// <summary>
        /// 附属层
        /// </summary>
        public Transform _Attached { private set; get; }
        /// <summary>
        /// 对话层
        /// </summary>
        public Transform _Dialog { private set; get; }
        /// <summary>
        /// 弹窗层
        /// </summary>
        public Transform _Pop { private set; get; }
        /// <summary>
        /// 引导层
        /// </summary>
        public Transform _Guide { private set; get; }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialization()
        {
            UIName = "RootUI";
            _Background = transform.GetChild("_Background");
            _Normal = transform.GetChild("_Normal");
            _Attached = transform.GetChild("_Attached");
            _Dialog = transform.GetChild("_Dialog");
            _Pop = transform.GetChild("_Pop");
            _Guide = transform.GetChild("_Guide");
        }

        /// <summary>
        /// 设置UI所在位置
        /// </summary>
        public void SetUIPosition(BaseUI UI)
        {
            switch (UI._UIInfoData.PositionType)
            {
                case UI_PositionType._Background:
                    {
                        UI.transform.SetParent(_Background);
                    }
                    break;
                case UI_PositionType._Normal:
                    {
                        UI.transform.SetParent(_Normal);
                    }
                    break;
                case UI_PositionType._Attached:
                    {
                        UI.transform.SetParent(_Attached);
                    }
                    break;
                case UI_PositionType._Dialog:
                    {
                        UI.transform.SetParent(_Dialog);
                    }
                    break;
                case UI_PositionType._Pop:
                    {
                        UI.transform.SetParent(_Pop);
                    }
                    break;
                case UI_PositionType._Guide:
                    {
                        UI.transform.SetParent(_Guide);
                    }
                    break;
            }
            UI.ResetViewPosInfo();
        }
    }
}
