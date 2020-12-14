/*******
*
*     Title:
*           资源管理器
*     Description:
*           进一步封装XAsset的基础功能
*           1.整合进入UI框架
*           2.简化资源加载的流程
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.UI;
using libx;
using System.Collections;
using System.Collections.Generic;
using ProtoData;
using UnityEngine;
using DataGod;

namespace BaseMVCFrame.Manager
{
	public class ResourceMgr : BaseManager<ResourceMgr>
	{
        /// <summary>
        /// 重写初始化函数
        /// </summary>
        protected override void Initialization()
        {
    
        }

        /// <summary>
        /// 加载UI窗体
        /// </summary>
        /// <param name="UIName"></param>
        /// <returns></returns>
        public BaseUI LoadUIView(string UIName)
        {
            BaseUI baseUI = LoadAsset<BaseUI>(UIName);
            BaseUI CloneUI = GameObject.Instantiate(baseUI);
            CloneUI.name = UIName;
            return CloneUI;
        }

        /// <summary>
        /// 加载通用资源，无缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="UIName"></param>
        /// <returns></returns>
        //public T LoadAsset<T>(string ResName)
        //{
        //    RESOURCEPATH_ARRAY _RESOURCEPATH_ARRAY = LoadProtoDataArray<RESOURCEPATH_ARRAY>();
        //    if (_RESOURCEPATH_ARRAY.IsNull() || _RESOURCEPATH_ARRAY.items.Count == 0)
        //    {
        //        Debug.LogError("读取资源路径配置文件出错！");
        //        return default(T);
        //    }
            
        //    foreach (RESOURCEPATH path in _RESOURCEPATH_ARRAY.items)
        //    {
        //        if (path.ResourceName.GetString_UTF8() == ResName)
        //        {
        //            AssetRequest assetRequest = Assets.LoadAsset(path.Path.GetString_UTF8(),typeof(GameObject));
        //            if (assetRequest.IsNull())
        //            {
        //                Debug.LogError("资源请求器为Null!");
        //                return default(T);
        //            }
        //            else if (assetRequest.asset.IsNull())
        //            {
        //                Debug.LogError("资源请求器中的资源为Null!");
        //                return default(T);
        //            }
        //            else
        //            {
        //                GameObject AssetObj = assetRequest.asset as GameObject;
        //                return AssetObj.GetComponent<T>();
        //            }
        //            Debug.Log(path.ResourceName);
        //        }
        //    }
        //    Debug.LogError("资源配置表中缺失数据:"+ ResName);
        //    return default(T);
        //}
        public T LoadAsset<T>(string ResName)
        {
            List<AssetPathConfig> pathConfigs = DataGodTool.LoadTable(DataTableType.AssetPathConfig_DataTable) as List<AssetPathConfig>;
            if (pathConfigs.IsNull() || pathConfigs.Count == 0)
            {
                Debug.LogError("读取资源路径配置文件出错！");
                return default(T);
            }

            foreach (AssetPathConfig assetPath in pathConfigs)
            {
                if (assetPath.AssetName == ResName)
                {
                    AssetRequest assetRequest = Assets.LoadAsset(assetPath.AssetPath, typeof(GameObject));
                    if (assetRequest.IsNull())
                    {
                        Debug.LogError("资源请求器为Null!");
                        return default(T);
                    }
                    else if (assetRequest.asset.IsNull())
                    {
                        Debug.LogError("资源请求器中的资源为Null!");
                        return default(T);
                    }
                    else
                    {
                        GameObject AssetObj = assetRequest.asset as GameObject;
                        return AssetObj.GetComponent<T>();
                    }
                    Debug.Log(assetPath.AssetName);
                }
            }
            Debug.LogError("资源配置表中缺失数据:" + ResName);
            return default(T);
        }
        /// <summary>
        /// 加载Proto的配置文件数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T LoadProtoDataArray<T>()
        {
            return ProtoBufTool.ReadOneDataConfig<T>("Proto_ResourcePath");
        }
    }
}
