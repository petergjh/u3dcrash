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

namespace BaseMVCFrame.UI
{
	public class Enum_UIFrame
	{
        
    }
    /// <summary>
    /// UI位置种类
    /// </summary>
    public enum UI_PositionType
    {
        /// <summary>
        /// 背景层
        /// </summary>
        _Background,
        /// <summary>
        /// 普通层
        /// </summary>
        _Normal,
        /// <summary>
        /// 附属层
        /// </summary>
        _Attached,
        /// <summary>
        /// 对话层
        /// </summary>
        _Dialog,
        /// <summary>
        /// 弹窗层
        /// </summary>
        _Pop,
        /// <summary>
        /// 引导层
        /// </summary>
        _Guide
    }
    /// <summary>
    /// UI遮罩种类
    /// </summary>
    public enum UI_MaskType
    {
        /// <summary>
        /// 没有遮罩
        /// </summary>
        _NoMask,
        /// <summary>
        /// 透明遮罩
        /// </summary>
        _Transparent,
        /// <summary>
        /// 纯黑40%透明度遮罩
        /// </summary>
        _Black_40,
    }
    /// <summary>
    /// UI的显示种类
    /// </summary>
    public enum UI_ShowType
    {
        /// <summary>
        /// 普通UI
        /// </summary>
        _NormalUI,
        /// <summary>
        /// 隐藏其他UI
        /// </summary>
        _HideOtherUI,
        /// <summary>
        /// 弹窗UI，需要设置反向切换
        /// </summary>
        _PopUI
    }

    /// <summary>
    /// 加载UI的类型
    /// </summary>
    public enum LoadUIType
    {
        /// <summary>
        /// 通过资源文件夹加载
        /// </summary>
        Load_Resources,
        /// <summary>
        /// 通过XAsset加载
        /// </summary>
        Load_XAsset
    }


    /// <summary>
    /// 图集类型
    /// </summary>
    public enum AtlasType
    {
        TempleUI_Atlas,

        HappyBirthdayGame_Atlas
    }
}
