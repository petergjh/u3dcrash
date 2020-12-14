/*******
*
*     Title:
*           游戏初始化设置脚本
*     Description:
*           游戏初始化的入口脚本
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Manager;
using BaseMVCFrame.UI;
using libx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaseMVCFrame
{
	public class GameInitializer : MonoBehaviour
	{

        // Start is called before the first frame update
        void Start()
        {
            //初始化XAsset
            XAssetInitialize();

            //游戏管理器初始化
        }

        /// <summary>
        /// XAsset初始化函数
        /// </summary>
        private void XAssetInitialize()
        {
            //XAsset初始化
            Assets.Initialize(XAssetInitializeCompleted);
        }

        /// <summary>
        /// XAsset初始化完成后的回调函数,开始游戏
        /// </summary>
        private void XAssetInitializeCompleted(string InitializeInfo)
        {
            //加载初始界面
            UIManager.Instance.OpenUIVeiw(ProjectConsts.GameStartUI);

            //创建其他工具类
            CreateIEManager();

            //WorldTimeMgr.Instance.TheWorldTimeStart();

        }


        private void CreateIEManager()
        {
            GameObject obj = new GameObject("IEManager");
            obj.AddComponent<IEManager>();
            DontDestroyOnLoad(obj);
        }
    }
}
