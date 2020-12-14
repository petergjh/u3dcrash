/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.UI
{
	public class BaseUI : MonoBehaviour
	{
        /// <summary>
        /// UI的位置类型
        /// </summary>
        public UI_PositionType PositionType;
        /// <summary>
        /// UI的遮罩类型
        /// </summary>
        public UI_MaskType MaskType;
        /// <summary>
        /// UI的显示类型
        /// </summary>
        public UI_ShowType ShowType;
        /// <summary>
        /// UI的基础信息
        /// </summary>
        public UIInfoData _UIInfoData {protected set; get;}
        /// <summary>
        /// 附属窗体的List
        /// </summary>
        public List<string> _AttachedUIList { protected set; get; } = new List<string>();
        /// <summary>
        /// 初始位置
        /// </summary>
        protected Vector3 _InitPosition = new Vector3(0,0,0);
        /// <summary>
        /// 记录窗体名（兼顾消息名）
        /// </summary>
        public virtual string UIName { set; get; } = "";


        private void Awake()
        {
            //将修改后的窗体基础属性进行赋值
            _UIInfoData = new UIInfoData(PositionType, MaskType, ShowType);

            //执行初始化函数
            Initialization();
            //执行初始化按钮监听函数
            InitButtonListener();
        }


        /// <summary>
        /// 初始化函数
        /// </summary>
        protected virtual void Initialization()
        {

        }

        /// <summary>
        /// 初始化按钮监听
        /// </summary>
        protected virtual void InitButtonListener()
        {

        }





        #region 窗体控制方法
        /// <summary>
        /// 打开UI窗体
        /// </summary>
        protected void OpenUIView(string UIName)
        {
            UIManager.Instance.OpenUIVeiw(UIName);
        }
        /// <summary>
        /// 重设窗体位置信息
        /// </summary>
        public void ResetViewPosInfo()
        {
            //初始化位置
            transform.localPosition = _InitPosition;
            //初始化缩放
            transform.localScale = new Vector3(1, 1, 1);
        }
        #endregion

        #region 窗体的状态函数
        /// <summary>
        /// 窗体显示
        /// </summary>
        public void Display()
        {
            this.Show();
            DisplayInit();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        public void Close()
        {
            CloseInit();
            this.Hide();
        }
        #endregion

        #region 重写函数
        /// <summary>
        /// 显示时调用的初始化方法
        /// </summary>
        protected virtual void DisplayInit()
        {

        }
        /// <summary>
        /// 关闭时候调用的初始化方法
        /// </summary>
        protected virtual void CloseInit()
        {

        }
        #endregion

        #region UI功能函数


        #endregion

    }
}
