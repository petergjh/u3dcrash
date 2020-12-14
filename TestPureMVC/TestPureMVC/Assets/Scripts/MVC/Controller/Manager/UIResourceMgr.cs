/*******
*
*     Title:
*     Description:UI
*         资源管理器，通过配置文件获取加载UI预设
*     Date:
*     Version:
*     Modify Recoder:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoData;
using BaseMVCFrame.UI;
using UnityEngine.U2D;

namespace BaseMVCFrame.Manager
{
	public class UIResourceMgr : BaseManager<UIResourceMgr>
	{
        /// <summary>
        /// 加载UI资源的方式
        /// </summary>
        private LoadUIType _LoadUIType = LoadUIType.Load_Resources;

        /// <summary>
        /// 缓存UI预设资源的字典
        /// Describe:
        /// 缓存加载到的UI预设本体，如果UIManager需要创建预设的话先从这里查找是否有缓存
        /// 如果有缓存则节省了加载资源的过程，直接从缓存中克隆一个新的预设即可
        /// </summary>
        private Dictionary<string, BaseUI> _UIViewCache;

        /// <summary>
        /// 重写初始化函数
        /// </summary>
        protected override void Initialization()
        {
            _UIViewCache = new Dictionary<string, BaseUI>();
        }

        /// <summary>
        /// 废弃，暂时保留
        /// </summary>
        /// <param name="UIName"></param>
        public void LoadUI(string UIName)
        {
            UIPATH_ARRAY _UIPATH_ARRAY = ProtoBufTool.ReadOneDataConfig<UIPATH_ARRAY>("Proto_uipath");
            foreach (UIPATH path in _UIPATH_ARRAY.items)
            {
                if (path.UIName.GetString_UTF8() == UIName)
                {
                    GameObject UIObj = (GameObject)Resources.Load(path.Path.GetString_UTF8());
                    GameObject.Instantiate(UIObj);
                }
            }
           // string _Path = [];
        }

        /// <summary>
        /// 通过Resources文件夹加载UI窗体
        /// </summary>
        /// <param name="UIName">UI预设名</param>
        /// <param name="IsCache">是否添加到缓存中</param>
        /// <returns></returns>
        public BaseUI LoadUIView(string UIName,bool IsCache = false)
        {
            //查找是否有缓存资源，有则直接克隆一个缓存的资源
            BaseUI baseUI = null;
            _UIViewCache.TryGetValue(UIName,out baseUI);
            //如果缓存中没有该UI预设，通过配置路径读取资源
            switch (_LoadUIType)
            {
                case LoadUIType.Load_Resources:
                    {
                        baseUI = LoadUIByResources(UIName, IsCache);
                    }
                    break;
            }
            //返回克隆的UI预设
            BaseUI CloneUI = GameObject.Instantiate(baseUI);
            CloneUI.name = UIName;
            return CloneUI;
        }

        /// <summary>
        /// 通过资源文件夹加载UI
        /// </summary>
        /// <param name="UIName"></param>
        /// <param name="IsCache"></param>
        /// <returns></returns>
        private BaseUI LoadUIByResources(string UIName,bool IsCache)
        {
            BaseUI baseUI = null;
            //获得UI预设的配置路径文件
            UIPATH_ARRAY _UIPATH_ARRAY = ProtoBufTool.ReadOneDataConfig<UIPATH_ARRAY>("Proto_uipath");
            foreach (UIPATH path in _UIPATH_ARRAY.items)
            {
                if (path.UIName.GetString_UTF8() == UIName)
                {
                    //通过路径配置文件加载UI预设
                    GameObject UIObj = (GameObject)Resources.Load(path.Path.GetString_UTF8());
                    //获取UI基类
                    UIObj.TryGetComponent<BaseUI>(out baseUI);
                    //如果获取UI基类失败
                    if (baseUI.IsNull())
                    {
                        Debug.LogError("获取加载的UI预设的UI基类失败！检查预设是否已经添加了基类脚本！");
                    }
                    //判断是否添加到缓存字典中
                    if (IsCache)
                    {
                        //如果缓存中没有则添加到缓存中
                        if (!_UIViewCache.ContainsKey(UIName))
                        {
                            _UIViewCache.Add(UIName, baseUI);
                        }
                    }
                    break;
                }
            }
            return baseUI;
        }

    }
}
