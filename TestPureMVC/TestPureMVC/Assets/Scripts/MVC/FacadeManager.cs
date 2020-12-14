/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame.Manager
{
  

	//public class FacadeManager : BaseManager<FacadeManager>
    public class FacadeManager : Facade
	{
        public FacadeManager()
        {
            //注册核心的“命令”
            //RegisterCommand(ProjectConsts.Reg_StartGameCommand,typeof(Ctrl_StartGameCommond));
            RegisterCommand(ProjectConsts.Reg_StartGameCommand, typeof(Ctrl_ReStartGameCommond));

            RegisterCommand(ProjectConsts.Reg_EndGameCommand, typeof(Ctrl_EndGameCommand));

            //注册“模型层”与“视图层”
            RegisterProxy(new Model_GameDataProxy());
            RegisterMediator(new Vew_GamePlayingMediator());

            //添加游戏对象脚本
            AddGameObjectScript();
        }

        /// <summary>
        /// 添加游戏对象脚本
        /// </summary>
	    private void AddGameObjectScript()
        {
            //得到层级视图的根对象
            GameObject goEnviromentRoot = GameObject.Find(ProjectConsts.MainGameScenes);
            //添加主角控制脚本
            //GameObject.FindGameObjectWithTag(ProjectConsts.Player).AddComponent<Ctrl_HeroControl>();
            //动态挂载陆地移动脚本
            //UnityHelper.AddChildNodeCompnent<Ctrl_LandMoving>(goEnviromentRoot, "GameLanding");
            //动态挂载管道组移动脚本
            //UnityHelper.AddChildNodeCompnent<Ctrl_PipesMoving>(goEnviromentRoot, "GamePipes");
            //动态挂载管道道具与“地面”
            for (int i = 1; i <= 3; i++)
            {
                //UnityHelper.AddChildNodeCompnent<Ctrl_PipeAndLand>(goEnviromentRoot, "Pipe" + i + "_Down");
                //UnityHelper.AddChildNodeCompnent<Ctrl_PipeAndLand>(goEnviromentRoot, "Pipe" + i + "_Up");
                //UnityHelper.AddChildNodeCompnent<Ctrl_PipeAndLand>(goEnviromentRoot, "Landing" + i);
            }
            //动态挂载“金币”（隐藏的）道具
            for (int i = 1; i <= 3; i++)
            {
                //UnityHelper.AddChildNodeCompnent<Ctrl_Golds>(goEnviromentRoot, "Pipe" + i + "_Trigger");
            }

            //动态挂载“得到时间”脚本
            //goEnviromentRoot.AddComponent<Ctrl_GetTime>();
        }
    }
}
