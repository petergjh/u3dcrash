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
using BaseMVCFrame.Manager;

namespace BaseMVCFrame.UI
{
	public class UIManager : BaseManager<UIManager>
	{
        /// <summary>
        /// 所有缓存的UI窗体
        /// </summary>
        private Dictionary<string, BaseUI> _AllSavedUIView;
        /// <summary>
        /// 当前显示的UI窗体
        /// </summary>
        private Dictionary<string, BaseUI> _CurrentUIView;
        /// <summary>
        /// 弹窗的栈
        /// </summary>
        private Stack<BaseUI> _StackPopUIView;
        /// <summary>
        /// UI根节点
        /// </summary>
        public RootUI _RootUI { private set; get; }

        /// <summary>
        /// 重写初始化函数
        /// </summary>
        protected override void Initialization()
        {
            _AllSavedUIView = new Dictionary<string, BaseUI>();
            _CurrentUIView = new Dictionary<string, BaseUI>();
            _StackPopUIView = new Stack<BaseUI>();

            //实例化UI根节点，并且保证全局唯一
            _RootUI = ResourceMgr.Instance.LoadUIView("RootUI").GetComponent<RootUI>();
            GameObject.DontDestroyOnLoad(_RootUI.gameObject);
        }

        /// <summary>
        /// 打开一个窗体
        /// </summary>
        public void OpenUIVeiw(string UIName)
        {
            BaseUI baseUI = null;
            //查找缓存中是否有该UI窗体
            baseUI = TryGetUIFromAllSavedUIView(UIName);
            //如果没有缓存数据，则通过路径查找并生成新的UI
            if (baseUI.IsNull())
            {
                //通过UI窗体名加载UI预设
                baseUI = LoadUIVeiwToCache(UIName);
            }

            //处理窗体显示种类
            switch (baseUI._UIInfoData.ShowType)
            {
                case UI_ShowType._NormalUI:
                    {
                        //通常UI窗体，只显示
                        ShowNormalUIView(baseUI);
                    }
                    break;
                case UI_ShowType._HideOtherUI:
                    {
                        //需要隐藏其它窗体
                        ShowHiderOtherUIView(baseUI);
                    }
                    break;
                case UI_ShowType._PopUI:
                    {
                        //弹窗，需要添加到反向切换的栈
                        ShowPopUIView(baseUI);
                    }
                    break;
            }

            //加载附属窗体
            if (baseUI._AttachedUIList.IsNotNull() && baseUI._AttachedUIList.Count>0)
            {
                for (int i = 0;i< baseUI._AttachedUIList.Count;i++)
                {
                    string AttachedUIName = baseUI._AttachedUIList[i];
                    BaseUI AttachedUI = null;
                    //判断缓存中是否有该附属窗体
                    AttachedUI = TryGetUIFromAllSavedUIView(AttachedUIName);
                    //如果缓存中没有数据，则加载新的窗体到缓存中
                    if (AttachedUI.IsNull())
                    {
                        //加载附属UI
                        AttachedUI = LoadUIVeiwToCache(AttachedUIName);
                    }
                    //添加到当前正在显示的UI字典中缓存
                    if (_CurrentUIView.ContainsKey(AttachedUIName))
                    {
                        Debug.LogError("添加附属UI到当前显示的字典中失败！已经有缓存数据!UIName:"+ AttachedUIName);
                    }
                    _CurrentUIView.Add(AttachedUIName, AttachedUI);
                    AttachedUI.Display();
                }
            }
            //设置UI遮罩

        }

        /// <summary>
        /// 关闭一个UI窗体
        /// </summary>
        /// <param name="UIName">要关闭的UI名</param>
        /// <param name="IsRelease">true:释放该窗体资源 false:设置该窗体资源状态为不活跃</param>
        protected void CloseUIView(string UIName,bool IsRelease = false)
        {

        }

        /// <summary>
        /// 尝试从缓存字典中获取UI基类
        /// </summary>
        /// <returns></returns>
        private BaseUI TryGetUIFromAllSavedUIView(string UIName)
        {
            if (UIName.IsNotNullAndEmpty() && _AllSavedUIView.Count > 0)
            {
                BaseUI baseUI = null;
                _AllSavedUIView.TryGetValue(UIName,out baseUI);
                return baseUI;
            }
            return null;
        }
        /// <summary>
        /// 加载UI窗体到缓存中
        /// </summary>
        /// <param name="UIName"></param>
        /// <returns></returns>
        private BaseUI LoadUIVeiwToCache(string UIName)
        {
            //通过UI窗体名加载UI预设
            BaseUI baseUI = ResourceMgr.Instance.LoadUIView(UIName);
            //如果加载失败
            if (baseUI.IsNull())
            {
                Debug.LogError("加载窗体预设失败，请检查窗体名和文件路径.加载的窗体名:" + UIName);
            }
            else
            {
                if (_AllSavedUIView.ContainsKey(UIName))
                {
                    Debug.LogError("缓存窗体预设失败，字典中已经有同名缓存！缓存窗体名:" + UIName);
                }
                //缓存窗体
                _AllSavedUIView.Add(UIName, baseUI);
            }
            //设置窗体所在层级
            _RootUI.SetUIPosition(baseUI);
            return baseUI;
        }

        /// <summary>
        /// 显示普通UI
        /// </summary>
        /// <param name="baseUI"></param>
        private void ShowNormalUIView(BaseUI baseUI)
        {
            if (!_CurrentUIView.ContainsKey(baseUI.UIName))
            {
                _CurrentUIView.Add(baseUI.UIName, baseUI);
            }
            baseUI.Display();
        }
        /// <summary>
        /// 显示隐藏其它UI
        /// </summary>
        /// <param name="baseUI"></param>
        private void ShowHiderOtherUIView(BaseUI baseUI)
        {
            //遍历字典关闭其他UI窗体
            _CurrentUIView.ForEach((key,value)=> 
            {
                value.Close();
            });
            //清空缓存字典数据
            _CurrentUIView.Clear();
            //将窗体添加到显示字典中
            if (_CurrentUIView.ContainsKey(baseUI.UIName))
            {
                Debug.LogError("添加窗体到当前正显示字典中失败，字典中已经有同名缓存！缓存窗体名:" + baseUI.UIName);
            }
            _CurrentUIView.Add(baseUI.UIName, baseUI);
            //显示当前窗体
            baseUI.Display();
        }
        /// <summary>
        /// 显示弹窗UI
        /// </summary>
        /// <param name="baseUI"></param>
        private void ShowPopUIView(BaseUI baseUI)
        {
            //将UI添加到弹窗层的栈
            _StackPopUIView.Push(baseUI);
            //显示当前UI窗体
            baseUI.Display();
        }
    }
}
