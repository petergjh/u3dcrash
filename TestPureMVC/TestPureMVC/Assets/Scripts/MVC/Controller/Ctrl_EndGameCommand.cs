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
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;


namespace BaseMVCFrame.Manager
{
    public class Ctrl_EndGameCommand : SimpleCommand
    {


        public override void Execute(INotification notification)
        {
            //停止脚本运行。
            StopScriptRuning();
            //关闭当前UI窗体，回到“玩家指导”UI窗体
            CloseCurrentUIForm();
            //保存当前最高分数
            Model_GameDataProxy gameData = Facade.RetrieveProxy(Model_GameDataProxy.NAME) as Model_GameDataProxy;
            gameData.SaveHightestScores();
            //重置游戏时间
            gameData.ResetGameTime();
        }

        /// <summary>
        /// 停止脚本运行
        /// </summary>
	    private void StopScriptRuning()
        {
            //主角停止运行
            //GameObject.FindGameObjectWithTag(ProjectConsts.Player).GetComponent<Ctrl_HeroControl>().StopGame();
            //得到层级视图根节点。
            GameObject goEnviromentRoot = GameObject.Find(ProjectConsts.MainGameScenes);
            //管道组停止运行
            //UnityHelper.FindTheChildNode(goEnviromentRoot, "GamePipes").GetComponent<Ctrl_PipesMoving>().StopGame();
            //获取时间脚本,然后停止运行
            //goEnviromentRoot.GetComponent<Ctrl_GetTime>().StopGame();
            //“金币”道具停止
            for (int i = 1; i <= 3; i++)
            {
                //道具开始运行               
                //UnityHelper.FindTheChildNode(goEnviromentRoot, "Pipe" + i + "_Trigger").GetComponent<Ctrl_Golds>().StopGame();
            }

        }

        // 关闭当前UI窗体，回到“玩家指导”UI窗体
        private void CloseCurrentUIForm()
        {
            //UIManager.GetInstance().CloseUIForms(ProjectConsts.GamePlayingUIForm);
        }
    }
}